using System.Media;


ApplicationConfiguration.Initialize();

var start = new Start();
SoundPlayer simpleSound = new SoundPlayer("Sounds/Soundtrack.wav");
simpleSound.Play();
start.go();
