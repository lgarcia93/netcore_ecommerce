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
      cpu       = 200
      memory    = 100
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
      cpu       = 200
      memory    = 100
      essential = true
      portMappings = [
        {
          containerPort = 6000
          hostPort      = 6000
        }
      ]
    },
    {
      name      = "cart-service"
      image     = "${aws_ecr_repository.default.repository_url}:cart-service-v1"
      cpu       = 200
      memory    = 100
      essential = true
      portMappings = [
        {
          containerPort = 5001
          hostPort      = 5001
        }
      ]
    },
    {
      name      = "order-service"
      image     = "${aws_ecr_repository.default.repository_url}:order-service-v1"
      cpu       = 200
      memory    = 100
      essential = true
      portMappings = [
        {
          containerPort = 5030
          hostPort      = 5030
        }
      ]
    }
  ])
}

resource "aws_ecs_service" "default" {
  name        = "ecom-ecs-service"
  cluster     = aws_ecs_cluster.ecom_cluster.id
  launch_type = "EC2"

  task_definition = aws_ecs_task_definition.ecom_ecs_task.arn
  desired_count   = 1
}