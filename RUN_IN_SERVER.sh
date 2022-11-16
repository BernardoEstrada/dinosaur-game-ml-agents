#!/bin/bash
if (($# != 1))
then
  echo "Usage: $0 [run-id]"
else
#screen \
mlagents-learn \
Assets/Dino.yaml \
--env=./LINUX_SERVER_DINO/dino-ml-training.x86_64 \
--num-envs=2 \
--num-areas=30 \
--run-id={$1} \
--no-graphics \
--torch-device=cuda
fi
