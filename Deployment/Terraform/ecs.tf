resource "aws_ecr_repository" "default" {
  name                 = "ecom"
  image_tag_mutability = "IMMUTABLE"

    provisioner "local-exec" {
      command = "./build-images.sh ${local.account_id}"
    }
}

resource "aws_ecr_repository_policy" "ecom_repo_policy" {
  repository = aws_ecr_repository.default.name
  policy     = <<EOF
{
    "Version": "2008-10-17",
    "Statement": [
        {
            "Sid": "new policy",
            "Effect": "Allow",
            "Principal": "*",
            "Action": [
                "ecr:GetDownloadUrlForLayer",
                "ecr:BatchGetImage",
                "ecr:BatchCheckLayerAvailability",
                "ecr:PutImage",
                "ecr:InitiateLayerUpload",
                "ecr:UploadLayerPart",
                "ecr:CompleteLayerUpload",
                "ecr:DescribeRepositories",
                "ecr:GetRepositoryPolicy",
                "ecr:ListImages",
                "ecr:DeleteRepository",
                "ecr:BatchDeleteImage",
                "ecr:SetRepositoryPolicy",
                "ecr:DeleteRepositoryPolicy"
            ]
        }
    ]
}
EOF
}

resource "aws_ecs_cluster" "ecom_cluster" {
  name = "ecom"
}

resource "aws_ecs_task_definition" "ecom_ecs_task" {
  family       = "ecommerce_family"
  network_mode = "host"

  requires_compatibilities = ["EC2"]
  
  container_definitions = jsonencode([
    {
      name      = "product-service"
      image     = "${aws_ecr_repository.default.repository_url}:product-service-v1"
      cpu       = 300
      memory    = 128
      essential = true
      portMappings = [
        {
          containerPort = 5000
          hostPort      = 5000
        }
      ]
    },
    {
      name      = "user-service"
      image     = "${aws_ecr_repository.default.repository_url}:user-service-v1"
      cpu       = 300
      memory    = 128
      essential = true
      portMappings = [
        {
          containerPort = 6000
          hostPort      = 6000
        }
      ]
    }    
  ])
}

resource "aws_ecs_service" "default" {
  name        = "ecom-ecs-service"
  cluster     = aws_ecs_cluster.ecom_cluster.id
  launch_type = "EC2"
#  network_configuration {
#    subnets = [
#      aws_subnet.ecommerce-subnet-private-1.id,
#      aws_subnet.ecommerce-subnet-private-2.id
#    ]
#  }

  task_definition = aws_ecs_task_definition.ecom_ecs_task.arn  
  desired_count   = 1
}