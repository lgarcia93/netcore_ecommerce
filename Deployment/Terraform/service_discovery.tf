resource "aws_service_discovery_http_namespace" "ecommerce-app" {
  name = "ecommerce-app"
}

resource "aws_service_discovery_service" "ecommerce-product-service" {
  name = "product-service"

  namespace_id = aws_service_discovery_http_namespace.ecommerce-app.id
}

resource "aws_service_discovery_service" "ecommerce-user-service" {
  name = "user-service"

  namespace_id = aws_service_discovery_http_namespace.ecommerce-app.id
}

resource "aws_service_discovery_service" "ecommerce-cart-service" {
  name = "cart-service"

  namespace_id = aws_service_discovery_http_namespace.ecommerce-app.id
}

resource "aws_service_discovery_service" "ecommerce-order-service" {
  name = "order-service"

  namespace_id = aws_service_discovery_http_namespace.ecommerce-app.id
}

resource "aws_service_discovery_instance" "product-service-instance" {
  instance_id = "product-service-instance"
  service_id  = aws_service_discovery_service.ecommerce-product-service.id

  depends_on = [aws_instance.ecommerce_machine]

  attributes = {
    AWS_INSTANCE_IPV4 = aws_instance.ecommerce_machine.private_ip
    SERVICE_PORT      = 5000
  }
}

resource "aws_service_discovery_instance" "user-service-instance" {
  instance_id = "user-service-instance"
  service_id  = aws_service_discovery_service.ecommerce-user-service.id

  depends_on = [aws_instance.ecommerce_machine]

  attributes = {
    AWS_INSTANCE_IPV4 = aws_instance.ecommerce_machine.private_ip
    SERVICE_PORT      = 6000
  }
}

resource "aws_service_discovery_instance" "cart-service-instance" {
  instance_id = "cart-service-instance"
  service_id  = aws_service_discovery_service.ecommerce-user-service.id

  depends_on = [aws_instance.ecommerce_machine]

  attributes = {
    AWS_INSTANCE_IPV4 = aws_instance.ecommerce_machine.private_ip
    SERVICE_PORT      = 5001
  }
}

resource "aws_service_discovery_instance" "order-service-instance" {
  instance_id = "order-service-instance"
  service_id  = aws_service_discovery_service.ecommerce-user-service.id

  depends_on = [aws_instance.ecommerce_machine]

  attributes = {
    AWS_INSTANCE_IPV4 = aws_instance.ecommerce_machine.private_ip
    SERVICE_PORT      = 5030
  }
}