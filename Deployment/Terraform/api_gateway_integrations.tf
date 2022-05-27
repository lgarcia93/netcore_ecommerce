resource "aws_apigatewayv2_integration" "apigateway_private_integration_products" {
  api_id           = aws_apigatewayv2_api.ecommerceapi.id
  integration_type = "HTTP_PROXY"
  integration_uri = aws_lb_listener.nlb-listener-product-service.arn

  integration_method = "ANY"
  connection_type = "VPC_LINK"
  connection_id = aws_apigatewayv2_vpc_link.ecommerceapi_link.id
}

resource "aws_apigatewayv2_integration" "apigateway_private_integration_user" {
  api_id           = aws_apigatewayv2_api.ecommerceapi.id
  integration_type = "HTTP_PROXY"
  integration_uri = aws_lb_listener.nlb-listener-user-service.arn

  integration_method = "ANY"
  connection_type = "VPC_LINK"
  connection_id = aws_apigatewayv2_vpc_link.ecommerceapi_link.id
}