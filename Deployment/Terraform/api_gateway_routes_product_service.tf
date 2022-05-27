resource "aws_apigatewayv2_route" "route_products_get_all" {
  api_id    = aws_apigatewayv2_api.ecommerceapi.id
  route_key = "GET /api/product"
  
  target = "integrations/${aws_apigatewayv2_integration.apigateway_private_integration_products.id}"
}

resource "aws_apigatewayv2_route" "route_products_get_by_id" {
  api_id    = aws_apigatewayv2_api.ecommerceapi.id
  route_key = "GET /api/product/{id}"

  target = "integrations/${aws_apigatewayv2_integration.apigateway_private_integration_products.id}"
}

resource "aws_apigatewayv2_route" "route_products_create" {
  api_id    = aws_apigatewayv2_api.ecommerceapi.id
  route_key = "POST /api/product"

  target = "integrations/${aws_apigatewayv2_integration.apigateway_private_integration_products.id}"
}

resource "aws_apigatewayv2_route" "route_products_delete" {
  api_id    = aws_apigatewayv2_api.ecommerceapi.id
  route_key = "DELETE /api/product/{id}"

  target = "integrations/${aws_apigatewayv2_integration.apigateway_private_integration_products.id}"
}

resource "aws_apigatewayv2_route" "route_products_update" {
  api_id    = aws_apigatewayv2_api.ecommerceapi.id
  route_key = "PUT /api/product/{id}"

  target = "integrations/${aws_apigatewayv2_integration.apigateway_private_integration_products.id}"
}