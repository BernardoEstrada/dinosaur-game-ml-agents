using System;
using System.Text.RegularExpressions;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Policies;
using UnityEngine;
using Unity.MLAgents.Sensors;


namespace ML {
    public class DinoAgent : Agent {
        public float rewardForStayingAlive = 1f;
        public float penaltyForDying = -1f;
        public float penaltyForJumping = -0.01f;
        public float penaltyForCrouching = -0.01f;
        

        [HideInInspector]
        JumpAndDuck mAgentJumpAndDuck;
        private bool crashed;
        private float distanceToNearest;
        private RaycastHit raycastHit;
        EnvironmentParameters mResetParams;
        Rigidbody2D agentRb;
        Level level;
        Scores scores;

        public override void Initialize() {
            scores = transform.parent.GetComponentInChildren<Scores>();
            level = transform.parent.GetComponentInChildren<Level>();

            
            if (scores == null || level == null) {
                throw new MissingReferenceException("Include Scores And Level on your environment");
            }
            distanceToNearest = 1000;
            crashed = false;
            agentRb = GetComponent<Rigidbody2D>();
            mAgentJumpAndDuck = GetComponent<JumpAndDuck>();
            mResetParams = Academy.Instance.EnvironmentParameters;
            // decisionRequester = GetComponent<DecisionRequester>();
            // rayPerceptionSensor = GetComponent<RayPerceptionSensorComponent2D>().RaySensor;
            
            GetComponent<BehaviorParameters>().TeamId = level.localInstanceId;
            SetResetParameters();
        }
        
        public void CrashedWithObstacle() {
            crashed = true;
            mAgentJumpAndDuck.Reset();
            GetComponent<HitObstacles>().Reset();
            AddReward(penaltyForDying);
            level.Restart();
            EndEpisode();
        }

        public void JumpAgent(ActionBuffers act) {
            var jump = act.DiscreteActions[0] == 1;
            var crouch = act.DiscreteActions[1] == 1;
            var stand = act.DiscreteActions[2] == 1;
            if (jump) {
                mAgentJumpAndDuck.jump();
                AddReward(penaltyForJumping);
            }

            if (crouch) {
                mAgentJumpAndDuck.duck();
                AddReward(penaltyForCrouching);
            }
            if (stand) mAgentJumpAndDuck.stand();
        }

        public override void CollectObservations(VectorSensor sensor) {
            sensor.AddObservation(level.mainSpeed);
            sensor.AddObservation(scores.Score);
            sensor.AddObservation(distanceToNearest);
            sensor.AddObservation(mAgentJumpAndDuck.JumpVelocity);
            sensor.AddObservation(mAgentJumpAndDuck.Grounded);
            sensor.AddObservation(mAgentJumpAndDuck.Ducking);
        }

        public override void OnActionReceived(ActionBuffers actionBuffers) {
            JumpAgent(actionBuffers);
            if (!crashed) { 
                // Add reward to encourage agent to stay alive biased with score and highscore to encourage agent to get highscore
                var scoreBias = (scores.Score > 0 && scores.HighScore > 0)
                    ? (scores.Score / (float)scores.HighScore)
                    : 1;
                AddReward((rewardForStayingAlive / MaxStep) * scoreBias);
            }
        }

        public override void Heuristic(in ActionBuffers actionsOut) {
            var discreteActionsOut = actionsOut.DiscreteActions;
            discreteActionsOut[0] = Input.GetKey(KeyCode.Space) ? 1 : 0;
            discreteActionsOut[1] = Input.GetKey(KeyCode.DownArrow) ? 1 : 0;
            discreteActionsOut[2] = discreteActionsOut[0] + discreteActionsOut[1] == 0 ? 1 : 0;
        }

        public override void OnEpisodeBegin() {
            crashed = false;
            SetResetParameters();
        }

        void SetResetParameters() {
            mResetParams = Academy.Instance.EnvironmentParameters;
        }
    }
}