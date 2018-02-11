# Setup library project
project(AlternateVoice.Server VERSION 1.0.0 LANGUAGES CXX)

add_definitions(-DALTERNATEVOICE_EXPORTS)

# Add files to library
include_directories(../include)
include_directories(../thirdparty/enet/include)

file(GLOB SOURCES "./*.cpp")

add_library(AlternateVoice.Server SHARED ${SOURCES})

# Link required libraries
target_link_libraries(AlternateVoice.Server enet)

if(WIN32)
target_link_libraries(AlternateVoice.Server ws2_32)
target_link_libraries(AlternateVoice.Server winmm)
endif(WIN32)

# Setup install directories
install(TARGETS AlternateVoice.Server ARCHIVE DESTINATION lib LIBRARY DESTINATION lib RUNTIME DESTINATION bin)
install(DIRECTORY ${CMAKE_SOURCE_DIR}/include/ DESTINATION include/AlternateVoice)
