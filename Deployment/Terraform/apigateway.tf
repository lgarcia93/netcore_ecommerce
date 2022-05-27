resource "aws_apigatewayv2_api" "ecommerceapi" {
  name          = "EcommerceApi"
  protocol_type = "HTTP"  
}
#
#
resource "aws_apigatewayv2_stage" "ecommerce_api_dev" {
  api_id = aws_apigatewayv2_api.ecommerceapi.id
  name   = "$default"
   auto_deploy = true
}
#

#s
#
resource "aws_apigatewayv2_vpc_link" "ecommerceapi_link" {
  name               = "ecomm"
  security_group_ids = [aws_security_group.vpc_link_sg.id]
  subnet_ids         = [aws_subnet.ecommerce-subnet-private-1.id, aws_subnet.ecommerce-subnet-private-2.id]
}
#
resource "aws_security_group" "vpc_link_sg" {
  name = "vpclink-sg"
  vpc_id = aws_vpc.ecommerce_vpc.id

  egress {
    from_port   = 0
    to_port     = 0
    protocol    = "-1"
    cidr_blocks = ["0.0.0.0/0"]
  }

  ingress {
    from_port = 80
    protocol  = "tcp"
    to_port   = 80
    cidr_blocks = ["0.0.0.0/0"]
  }
}
