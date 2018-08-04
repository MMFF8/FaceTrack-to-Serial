# FaceTrack-to-Serial
Takes webcam input, draws a box around your face, gets the midpoint of that box, and sends it to a serial port

# Current Flaws
  - Meant for tracking a single face only
  - Coordinate output trips over itself if >1 faces are detected
  - Could use a better haar cascade
  - Eye tracking is innacurate and useless at the moment
  - Ordinate is offset (the actual middle of the image isn't (0,0))
  - Could definately be written better
  - Needs code for the Arduino as well
  
