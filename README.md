# FaceTrack-to-Serial
Takes webcam input with EmguCV, draws a box around your face using the haar cascades, gets the midpoint of that box using the midpoint formula, and sends it to an Arduino or a Raspberry Pi using the serial port

\***The cascades and EmguCV are not mine (duh), credit goes to their respective creators***

# Current Flaws
  - Meant for tracking a single face only
  - Coordinate output trips over itself if >1 faces are detected
  - Could use a better haar cascade
  - Eye tracking is innacurate and useless at the moment
  - Ordinate is offset (the actual middle of the image isn't (0,0))
  - Could definately be written better
  - Needs code for the Arduino as well
  
