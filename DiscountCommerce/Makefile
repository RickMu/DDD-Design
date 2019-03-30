.DEFAULT_GOAL := code-test

.PHONY: create-test-images
create-test-images:
    docker build -f ci-cd/Dockerfile -t discount-commerce:latest .

.PHONY: code-test
code-test: create-test-images
    docker run --rm -v "${PWD}:/app" -w "/app" discount-commerce:latest dotnet test DomainTests 