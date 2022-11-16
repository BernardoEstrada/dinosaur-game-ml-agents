#!/bin/bash
if (($# < 1))
then
  echo "Usage: $0 [run-id] [other flags]"
else
id=$1
shift
#screen \
mlagents-learn \
Assets/Dino.yaml \
--env=./build/LINUX_SERVER_DINO/dino-ml-training.x86_64 \
--num-envs=2 \
--num-areas=30 \
--run-id={$id} \
--no-graphics \
--torch-device=cuda \
$@
fi
