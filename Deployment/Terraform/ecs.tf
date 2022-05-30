resource "aws_ecr_repository" "default" {
  name = "Ecom ECR Repository"
  image_tag_mutability = "IMMUTABLE"  
}

resource "aws_ecr_repository_policy" "ecom_repo_policy" {
  repository = aws_ecr_repository.default
  policy = <<EOF
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
  name = "Ecommerce Cluster"
}

resource "aws_launch_template" "" {}

resource "aws_ecs_task_definition" "ecom_ecr_task" {
  family                = "ecommerce_family"
  network_mode = "awsvpc"
  
  requires_compatibilities = ["EC2"]
  
  
  count = 1
  container_definitions = jsonencode([
    {
      name      = "user-service"
      image     = "user-service:v1"
      cpu       = 1
      memory    = 256
      essential = true
      portMappings = [
        {
          containerPort = 5000
          hostPort      = 5000
        }
      ]
    },
    {
      name      = "product-service"
      image     = "product-service:v1"
      cpu       = 1
      memory    = 256
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
  name = "Ecom ECS Service"
  cluster = aws_ecs_cluster.ecom_cluster
  launch_type = ""
  
  task_definition = aws_ecs_task_definition.ecom_ecr_task.arn
  count = 1
  desired_count = 1
  
}