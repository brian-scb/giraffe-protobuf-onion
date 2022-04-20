# Python Client

This is a simple client to demonstrate how to access the service.

## What it does

1. creates a request body as a protocol buffer
2. sends the body to the service via HTTP request
3. parses the response
4. prints some data from the response

## Setup

### Generate the protocol buffer files

```sh
# from the root folder (parent folder to the client)
buf generate
```

### Install dependencies

```sh
# from the client folder
pip install -r requirements.txt
```

### Run the client

```sh
# from the client folder
$ python client.py
name: Brian
message: Hello, Bub
```
