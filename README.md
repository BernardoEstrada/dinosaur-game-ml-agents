# Neural Network in a Unity Agent

## Chome dinosaur implementation with Neural Network using MLAgents and PyTorch

<img src="Assets/WebGLTemplates/GithubPagesTemplate/screenshot.png" width="500" />

This is an implementation fo chrome's dino game to which I added MLAgents library in order to train it using a Neural Network trained in PyTorch 


### Training

You must have the `mlagents` Python library installed (The dependencies are autoinstalled)
*-/-*9/9
For **Apple Silicon** I recommend using `Python 3.9.9` with `torch>=1.14.0` and `mlagents>=0.29.0` (You may have to install dependencies manually and change dependenvy verions of the libraries)

To train using my parameters use the following command:
```bash
mlagents-learn Assets/Dino.yaml [--env={game-build}] [--num-envs=2] --num-areas=30 --run-id=[run-id]
```
You can also use the following flags when running on a server:
`--no-graphics`
`--torch-device=cuda`

To show graphics with tensorboard run:
```bash
tensorboard --logdir results
```
You can add the flag `--bind_all` to bind the port to your local network

Dino game base forked from [Saiichi Shayan Hashimoto](https://github.com/saiichihashimoto/dinosaur-game)
> I wanted to learn how to make Unity games, so I picked an easy project. Copying [chrome's dinosaur game](https://twitter.com/googlechrome/status/723605173956800513) fit the bill. It has logic, sprites, graphics, etc while keeping it all super simple.
