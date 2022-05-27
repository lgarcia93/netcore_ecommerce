output "public-ip" {
  value = aws_instance.ecommerce_machine.public_ip
}

output "api_gateway_uri" {
  value = aws_apigatewayv2_api.ecommerceapi.api_endpoint
}