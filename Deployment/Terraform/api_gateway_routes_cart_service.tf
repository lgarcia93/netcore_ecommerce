resource "aws_apigatewayv2_route" "route_cart_load" {
  api_id    = aws_apigatewayv2_api.ecommerceapi.id
  route_key = "GET /api/cart"

  target = "integrations/${aws_apigatewayv2_integration.apigateway_private_integration_cart.id}"
}

resource "aws_apigatewayv2_route" "route_cart_clean" {
  api_id    = aws_apigatewayv2_api.ecommerceapi.id
  route_key = "DELETE /api/cart"

  target = "integrations/${aws_apigatewayv2_integration.apigateway_private_integration_cart.id}"
}

resource "aws_apigatewayv2_route" "route_cart_remove_product" {
  api_id    = aws_apigatewayv2_api.ecommerceapi.id
  route_key = "DELETE /api/cart/{id}"

  target = "integrations/${aws_apigatewayv2_integration.apigateway_private_integration_cart.id}"
}

resource "aws_apigatewayv2_route" "route_cart_radd_product" {
  api_id    = aws_apigatewayv2_api.ecommerceapi.id
  route_key = "POST /api/cart/"

  target = "integrations/${aws_apigatewayv2_integration.apigateway_private_integration_cart.id}"
}