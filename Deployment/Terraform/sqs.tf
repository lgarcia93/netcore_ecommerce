resource "aws_sqs_queue" "checkout_queue" {
  name                      = "checkout-queue"
}