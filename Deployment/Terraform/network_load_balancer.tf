resource "aws_lb" "network_ecommerce_loadbalancer" {
  name = "ecom-loadbalancer"
  internal = true
  load_balancer_type = "network"

  subnets = [aws_subnet.ecommerce-subnet-private-1.id, aws_subnet.ecommerce-subnet-private-2.id]
}

#Creating load balancer target group 
resource "aws_alb_target_group" "target-group-product-service" {
  name = "target-group-product-service"
  port = 5000
  protocol = "TCP"
  vpc_id = aws_vpc.ecommerce_vpc.id
}

#Creating load balancer target group 
resource "aws_alb_target_group" "target-group-user-service" {
  name = "target-group-user-service"
  port = 6000
  protocol = "TCP"
  vpc_id = aws_vpc.ecommerce_vpc.id
}

#Creating load balancer target group attachment (adding our EC2)
resource "aws_lb_target_group_attachment" "tg-attachment-product-service" {
  target_group_arn = aws_alb_target_group.target-group-product-service.arn
  target_id        = aws_instance.ecommerce_machine.id
  port = 5000
}

#Creating load balancer target group attachment (adding our EC2)
resource "aws_lb_target_group_attachment" "tg-attachment-user-service" {
  target_group_arn = aws_alb_target_group.target-group-user-service.arn
  target_id        = aws_instance.ecommerce_machine.id
  port = 6000
}

#Creating load balancer listener for product service
resource "aws_lb_listener" "nlb-listener-product-service" {
  load_balancer_arn = aws_lb.network_ecommerce_loadbalancer.arn  
  protocol = "TCP"
  port = 5000

  default_action {
    type = "forward"
    target_group_arn = aws_alb_target_group.target-group-product-service.arn
  }
}

#Creating load balancer listener for user service
resource "aws_lb_listener" "nlb-listener-user-service" {
  load_balancer_arn = aws_lb.network_ecommerce_loadbalancer.arn
  protocol = "TCP"
  port = 6000

  default_action {
    type = "forward"
    target_group_arn = aws_alb_target_group.target-group-user-service.arn
  }
}