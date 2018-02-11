#include <iostream>

#include "AlternateVoice.h"

int main() {
  // create server
  auto server = new AlternateVoice::Server(1234);
  if (server->create() == false) {
    std::cerr << "Unable to create AlternateVoice server on port 1234" << std::endl;
    return EXIT_FAILURE;
  }

  std::cout << "AlternateVoice server created on port 1234" << std::endl;

  // clean up
  server->close();
  delete server;

  return EXIT_SUCCESS;
}
