#include <iostream>

#include "JustAnotherVoiceChat.h"

int main() {
  // create server
  auto server = new JustAnotherVoiceChat::Server(1234);
  if (server->create() == false) {
    std::cerr << "Unable to create JustAnotherVoiceChat server on port 1234" << std::endl;
    return EXIT_FAILURE;
  }

  std::cout << "JustAnotherVoiceChat server created on port 1234" << std::endl;

  // clean up
  server->close();
  delete server;

  return EXIT_SUCCESS;
}
