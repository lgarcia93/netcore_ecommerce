variable "region" {
  description = "Infrastructure AWS region"
  default     = "us-east-1"
}

variable "name" {
  description = "Name of this microservice"
  default     = "Transaction service"
}

variable "env" {
  description = "Environment of the application"
  default     = "production"
}

variable "ami" {
  #default     = "ami-0c02fb55956c7d316"
  #ECS-Optimized
  default     = "ami-0dce57de6dcc3a6cc"
  description = "AMI to use in the EC2"
}
variable "instance_type" {
  description = "EC2 instance hardware"
  default     = "t2.micro"
}

variable "repo" {
  description = "Application repository"
  default     = "github.com/lgarcia93/app"
}