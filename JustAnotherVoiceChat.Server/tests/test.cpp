/*
 * File: tests/test.cpp
 * Date: 29.01.2018
 *
 * MIT License
 *
 * Copyright (c) 2018 JustAnotherVoiceChat
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

#include <iostream>
#include <enet/enet.h>

#ifdef _WIN32

#else
#include <csignal>
#endif

#include "justAnotherVoiceChat.h"
#include "testClient.h"

#include "../thirdparty/JustAnotherVoiceChat/include/protocol.h"

#include "test_api.h"

bool clientConnectedCallback(uint16_t clientId) {
  std::cout << "[TEST] Client connected " << clientId << std::endl;
  return true;
}

void clientTalkingChangedCallback(uint16_t clientId, bool talking) {
  std::cout << "[TEST] Client " << clientId << " changed talking " << talking << std::endl;
}

void clientMicrophoneMuteChangedCallback(uint16_t clientId, bool muted) {
  std::cout << "[TEST] Client " << clientId << " changed microphone mute to " << muted << std::endl;
}

void clientSpeakersMuteChangedCallback(uint16_t clientId, bool muted) {
  std::cout << "[TEST] Client " << clientId << " changed speakers mute to " << muted << std::endl;
}

void logMessage(const char *message, int level) {
  std::string levelString = "";

  switch (level) {
    case LOG_LEVEL_ERROR:
      levelString = "ERROR";
      break;

    case LOG_LEVEL_WARNING:
      levelString = "WARNING";
      break;

    case LOG_LEVEL_INFO:
      levelString = "INFO";
      break;

    case LOG_LEVEL_DEBUG:
      levelString = "DEBUG";
      break;
  }

  std::cout << "[" << levelString << "] " << message << std::endl;
}

#ifdef _WIN32

#else
void signalHandler(int signum) {
  JV_StopServer();
}
#endif

int main() {
  test_api();

#ifdef _WIN32

#else
  signal(SIGTERM, signalHandler);
  signal(SIGINT, signalHandler);
#endif
  
  // create server
  JV_CreateServer(ENET_PORT);
  JV_RegisterLogMessageCallback(logMessage);

  if (JV_StartServer() == false) {
    std::cerr << "[TEST] Unable to create JustAnotherVoiceChat server on port " << ENET_PORT << std::endl;
    return EXIT_FAILURE;
  }

  JV_RegisterClientConnectedCallback(clientConnectedCallback);
  JV_RegisterClientTalkingChangedCallback(clientTalkingChangedCallback);
  JV_RegisterClientMicrophoneMuteChangedCallback(clientMicrophoneMuteChangedCallback);
  JV_RegisterClientSpeakersMuteChangedCallback(clientSpeakersMuteChangedCallback);

  std::cout << "[TEST] JustAnotherVoiceChat server created on port " << ENET_PORT << std::endl;

  auto client = new TestClient(123, 456);
  if (client->connect("localhost", ENET_PORT) == false) {
    std::cout << "[TEST] Connection to localhost:" << ENET_PORT << " failed" << std::endl;
    return EXIT_FAILURE;
  }

  std::cout << "[TEST] Connection established" << std::endl;

  // main loop
  while (JV_IsServerRunning()) {
    client->update();
  }

  std::cout << "[TEST] Stopping JustAnotherVoiceChat server..." << std::endl;

  // clean up
  delete client;

  JV_StopServer();

  std::cout << "[TEST] Stopped JustAnotherVoiceChat server..." << std::endl;

  return EXIT_SUCCESS;
}
