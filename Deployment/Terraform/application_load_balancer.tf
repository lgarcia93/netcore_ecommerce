#resource "aws_lb" "ecommerce_loadbalancer" {
#  name = "ecom-loadbalancer"
#  internal = true
#  load_balancer_type = "network"
#  
#  subnets = [aws_subnet.ecommerce-subnet-private-1.id, aws_subnet.ecommerce-subnet-private-2.id]
#}
#
#resource "aws_lb_listener" "alb_products_listener" {
#  load_balancer_arn = aws_lb.ecommerce_loadbalancer.arn
#  port = "5000"
#  protocol = "HTTP"
#  
#  default_action {
#    type             = "forward"
#    target_group_arn = aws_alb_target_group.alb_produts_target_group.arn
#  }
#}
#
#resource "aws_alb_target_group" "alb_produts_target_group" {
#  name = "ecom-lb-target-group"
#  port = 5000
#  protocol = "HTTP"
#  vpc_id = aws_vpc.ecommerce_vpc.id
#}
#
#resource "aws_lb_target_group_attachment" "lb_target_instance" {
#  target_group_arn = aws_alb_target_group.alb_produts_target_group.arn
#  target_id        = aws_instance.ecommerce_machine.id
#  port = 5000
#}
#
#resource "aws_security_group" "alb_security_group" {
#  name   = "ecommerce_alg_sg"
#  vpc_id = aws_vpc.ecommerce_vpc.id
#
#  egress {
#    from_port   = 0
#    to_port     = 0
#    protocol    = "-1"
#    cidr_blocks = ["0.0.0.0/0"]
#  }
##  ingress {
##    description = "ProductService"
##    from_port   = 5000
##    protocol    = "tcp"
##    to_port     = 5000
##    cidr_blocks = ["0.0.0.0/0"]
##  }
#}
#
#resource "aws_security_group_rule" "sg_open_to_vpclink" {
#  from_port         = 5000
#  protocol          = "tcp"
#  security_group_id = aws_security_group.alb_security_group.id
#  to_port           = 5000
#  type              = "ingress"
#  source_security_group_id = aws_security_group.vpc_link_sg.id
#}