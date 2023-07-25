using System.Media;
var game = new Game();

SoundPlayer simpleSound = new SoundPlayer("Sounds/Soundtrack.wav");
simpleSound.Play();
game.go();