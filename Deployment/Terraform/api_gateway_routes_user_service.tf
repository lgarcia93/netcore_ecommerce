resource "aws_apigatewayv2_route" "route_login" {
  api_id    = aws_apigatewayv2_api.ecommerceapi.id
  route_key = "POST /api/login"

  target = "integrations/${aws_apigatewayv2_integration.apigateway_private_integration_user.id}"
}

resource "aws_apigatewayv2_route" "route_signup" {
  api_id    = aws_apigatewayv2_api.ecommerceapi.id
  route_key = "POST /api/user"

  target = "integrations/${aws_apigatewayv2_integration.apigateway_private_integration_user.id}"
}