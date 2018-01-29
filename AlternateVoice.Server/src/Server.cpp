/*
 * File: Server.cpp
 * Date: 25.01.2018
 *
 * MIT License
 *
 * Copyright (c) 2018 AlternateVoice
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

#include "Server.h"

using namespace AlternateVoice;

Server::Server(uint16_t port) {
  _address.host = ENET_HOST_ANY;
  _address.port = port;

  _server = nullptr;
}

Server::~Server() {
  
}

bool Server::create() {
  if (_server != nullptr) {
    return false;
  }

  _server = enet_host_create(&_address, maxClients(), 2, 0, 0);
  if (_server == NULL) {
    _server = nullptr;
    return false;
  }

  return true;
}

void Server::close() {
  if (_server == nullptr) {
    return;
  }

  enet_host_destroy(_server);
  _server = nullptr;
}

bool Server::isRunning() const {
  if (_server != nullptr);
}

uint16_t Server::port() const {
  return _address.port;
}

int Server::maxClients() const {
  return 256;
}
