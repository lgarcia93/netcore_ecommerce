resource "aws_lb" "network_ecommerce_loadbalancer" {
  name               = "ecom-nlb"
  internal           = true
  load_balancer_type = "network"

  subnets = [aws_subnet.ecommerce-subnet-private-1.id, aws_subnet.ecommerce-subnet-private-2.id]
}

#Creating load balancer listener for product service
resource "aws_lb_listener" "nlb-listener-product-service" {
  load_balancer_arn = aws_lb.network_ecommerce_loadbalancer.arn
  protocol          = "TCP"
  port              = 5000

  default_action {
    type             = "forward"
    target_group_arn = aws_lb_target_group.nlb-tg-5000-nlb.arn
  }
}

#Creating load balancer listener for user service
resource "aws_lb_listener" "nlb-listener-user-service" {
  load_balancer_arn = aws_lb.network_ecommerce_loadbalancer.arn
  protocol          = "TCP"
  port              = 6000

  default_action {
    type             = "forward"
    target_group_arn = aws_lb_target_group.nlb-tg-6000-nlb.arn
  }
}

resource "aws_lb_target_group" "nlb-tg-5000-nlb" {
  name = "nlb-tg-5000-alb"
  port = 5000
  protocol = "TCP"
  vpc_id = aws_vpc.ecommerce_vpc.id
  target_type = "alb"
}

resource "aws_lb_target_group" "nlb-tg-6000-nlb" {
  name = "nlb-tg-6000-alb"
  port = 6000
  protocol = "TCP"
  vpc_id = aws_vpc.ecommerce_vpc.id
  target_type = "alb"
}

#Creating load balancer target group attachment (forwarding to ALB)
resource "aws_lb_target_group_attachment" "tg-attachment-alb-5000" {
  target_group_arn = aws_lb_target_group.nlb-tg-5000-nlb.arn
  target_id        = aws_lb.ecom_alb.arn
  port             = 5000
}

##Creating load balancer target group attachment (forwarding to ALB)
resource "aws_lb_target_group_attachment" "g-attachment-alb-6000" {
  target_group_arn = aws_lb_target_group.nlb-tg-6000-nlb.arn
  target_id        = aws_lb.ecom_alb.arn
  port             = 6000
}
