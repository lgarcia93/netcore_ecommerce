resource "aws_security_group" "alb_security_group" {
  vpc_id = aws_vpc.ecommerce_vpc.id
  name   = "sg_ecom_alb"

  ingress {
    from_port   = 5000
    protocol    = "tcp"
    to_port     = 5000
    cidr_blocks = ["0.0.0.0/0"]
  }

  ingress {
    from_port   = 6000
    protocol    = "tcp"
    to_port     = 6000
    cidr_blocks = ["0.0.0.0/0"]
  }

  ingress {
    from_port   = 5001
    protocol    = "tcp"
    to_port     = 5001
    cidr_blocks = ["0.0.0.0/0"]
  }

  ingress {
    from_port   = 5030
    protocol    = "tcp"
    to_port     = 5030
    cidr_blocks = ["0.0.0.0/0"]
  }
}

resource "aws_security_group_rule" "sg_alb_egress_5000" {
  from_port                = 5000
  protocol                 = "tcp"
  security_group_id        = aws_security_group.alb_security_group.id
  to_port                  = 5000
  type                     = "egress"
  source_security_group_id = aws_security_group.ecommerce_machine_sg.id

}

resource "aws_security_group_rule" "sg_alb_egress_6000" {
  from_port                = 6000
  protocol                 = "tcp"
  security_group_id        = aws_security_group.alb_security_group.id
  to_port                  = 6000
  type                     = "egress"
  source_security_group_id = aws_security_group.ecommerce_machine_sg.id
}

resource "aws_security_group_rule" "sg_alb_egress_5001" {
  from_port                = 5001
  protocol                 = "tcp"
  security_group_id        = aws_security_group.alb_security_group.id
  to_port                  = 5001
  type                     = "egress"
  source_security_group_id = aws_security_group.ecommerce_machine_sg.id
}

resource "aws_security_group_rule" "sg_alb_egress_5030" {
  from_port                = 5030
  protocol                 = "tcp"
  security_group_id        = aws_security_group.alb_security_group.id
  to_port                  = 5030
  type                     = "egress"
  source_security_group_id = aws_security_group.ecommerce_machine_sg.id
}


resource "aws_lb" "ecom_alb" {
  name               = "ecom-alb"
  internal           = true
  load_balancer_type = "application"
  security_groups = [
    aws_security_group.alb_security_group.id
  ]

  depends_on = [aws_lb.network_ecommerce_loadbalancer]

  subnets = [aws_subnet.ecommerce-subnet-private-1.id, aws_subnet.ecommerce-subnet-private-2.id]
}

resource "aws_lb_target_group" "alb-tg-5000-alb" {
  name        = "alb-tg-5000-alb"
  port        = 5000
  protocol    = "HTTP"
  vpc_id      = aws_vpc.ecommerce_vpc.id
  target_type = "instance"
}

resource "aws_lb_target_group" "alb-tg-6000-alb" {
  name        = "alb-tg-6000-alb"
  port        = 6000
  protocol    = "HTTP"
  vpc_id      = aws_vpc.ecommerce_vpc.id
  target_type = "instance"
}

resource "aws_lb_target_group" "alb-tg-5001-alb" {
  name        = "alb-tg-5001-alb"
  port        = 5001
  protocol    = "HTTP"
  vpc_id      = aws_vpc.ecommerce_vpc.id
  target_type = "instance"
}

resource "aws_lb_target_group" "alb-tg-5030-alb" {
  name        = "alb-tg-5030-alb"
  port        = 5030
  protocol    = "HTTP"
  vpc_id      = aws_vpc.ecommerce_vpc.id
  target_type = "instance"
}


resource "aws_lb_listener" "alb_products_listener" {
  load_balancer_arn = aws_lb.ecom_alb.arn
  port              = 5000
  protocol          = "HTTP"

  default_action {
    type             = "forward"
    target_group_arn = aws_lb_target_group.alb-tg-5000-alb.arn
  }
}

resource "aws_lb_listener" "alb_user_listener" {
  load_balancer_arn = aws_lb.ecom_alb.arn
  port              = 6000
  protocol          = "HTTP"

  default_action {
    type             = "forward"
    target_group_arn = aws_lb_target_group.alb-tg-6000-alb.arn
  }
}

resource "aws_lb_listener" "alb_cart_listener" {
  load_balancer_arn = aws_lb.ecom_alb.arn
  port              = 5001
  protocol          = "HTTP"

  default_action {
    type             = "forward"
    target_group_arn = aws_lb_target_group.alb-tg-5001-alb.arn
  }
}

resource "aws_lb_listener" "alb_orders_listener" {
  load_balancer_arn = aws_lb.ecom_alb.arn
  port              = 5030
  protocol          = "HTTP"

  default_action {
    type             = "forward"
    target_group_arn = aws_lb_target_group.alb-tg-5030-alb.arn
  }
}


#Creating load balancer target group attachment (adding our EC2)
resource "aws_lb_target_group_attachment" "tg-attachment-product-service" {
  target_group_arn = aws_lb_target_group.alb-tg-5000-alb.arn
  target_id        = aws_instance.ecommerce_machine.id
  port             = 5000
}

##Creating load balancer target group attachment (adding our EC2)
resource "aws_lb_target_group_attachment" "tg-attachment-user-service" {
  target_group_arn = aws_lb_target_group.alb-tg-6000-alb.arn
  target_id        = aws_instance.ecommerce_machine.id
  port             = 6000
}

##Creating load balancer target group attachment (adding our EC2)
resource "aws_lb_target_group_attachment" "tg-attachment-cart-service" {
  target_group_arn = aws_lb_target_group.alb-tg-6000-alb.arn
  target_id        = aws_instance.ecommerce_machine.id
  port             = 5001
}

##Creating load balancer target group attachment (adding our EC2)
resource "aws_lb_target_group_attachment" "tg-attachment-orders-service" {
  target_group_arn = aws_lb_target_group.alb-tg-5030-alb.arn
  target_id        = aws_instance.ecommerce_machine.id
  port             = 5030
}