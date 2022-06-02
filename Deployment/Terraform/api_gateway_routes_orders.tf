resource "aws_apigatewayv2_route" "route_orders_get_orders" {
  api_id    = aws_apigatewayv2_api.ecommerceapi.id
  route_key = "GET /api/order"

  target = "integrations/${aws_apigatewayv2_integration.apigateway_private_integration_orders.id}"
}