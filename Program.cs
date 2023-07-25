

using System.Media;

var game = new Game();
SoundPlayer simpleSound = new SoundPlayer(@"c:\Windows\Media\chimes.wav");
simpleSound.Play();
game.go();