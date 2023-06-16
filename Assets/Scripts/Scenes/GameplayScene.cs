using System.Collections.Generic;
using Objects;
using TMPro;
using UnityEngine;
using Utils;
using Widgets;
using Random = UnityEngine.Random;

namespace Scenes
{
    public class GameplayScene : MonoBehaviour
    {
        private const float GroundSpeed = .5f;
        private const float Left = -1f;
        private const float PipeDelta = 1.5f;
        private const float PipeHeight = .25f;
        private const float PipeVelocity = .5f;
        [SerializeField] private Bird bird;
        [SerializeField] private Vector3 birdVelocity = Vector3.up * 1.4f;
        [SerializeField] private Transform floor;
        [SerializeField] private GameOverWidget gameOverWidget;
        [SerializeField] private Transform ground;
        [SerializeField] private PipePair[] pipes;
        [SerializeField] private TMP_Text scoreText;
        private readonly HashSet<int> _birdCollideObjects = new();
        private bool _gameOver;
        private int _score;

        private void Awake()
        {
            gameOverWidget.Visible = false;
        }

        private void BirdStep(float dt)
        {
            bird.Step(dt);
            if (bird.PositionY < floor.position.y)
            {
                bird.PositionY = floor.position.y;
            }
        }

        private void GroundStep(float dt)
        {
            ground.position += GroundSpeed * dt * Vector3.left;
            if (ground.position.x < -.84f)
            {
                var groundPosition = ground.position;
                groundPosition.x = .84f;
                ground.position = groundPosition;
            }
        }

        private void InputStep()
        {
            if (Input.GetMouseButtonDown(0))
            {
                bird.Velocity = birdVelocity;
            }
        }

        private void PipeStep(float dt)
        {
            foreach (var pipe in pipes)
            {
                pipe.Position += dt * PipeVelocity * Vector3.left;
                if (pipe.PositionX < Left)
                {
                    pipe.Position += new Vector3(pipes.Length * PipeVelocity * PipeDelta, Random.Range(-PipeHeight, PipeHeight));
                }

                foreach (var it in pipe.Pipes)
                {
                    if (PhysicsUtils.Overlap(bird.Position, bird.Radius, it.Position, it.Size))
                    {
                        _gameOver = true;
                        gameOverWidget.Visible = true;
                        scoreText.gameObject.SetActive(false);
                    }
                }

                if (_birdCollideObjects.Contains(pipe.GetInstanceID()))
                {
                    if (!PhysicsUtils.Overlap(bird.Position, bird.Radius, pipe.Position, pipe.Point))
                    {
                        _birdCollideObjects.Remove(pipe.GetInstanceID());
                    }
                }
                else
                {
                    if (PhysicsUtils.Overlap(bird.Position, bird.Radius, pipe.Position, pipe.Point))
                    {
                        _birdCollideObjects.Add(pipe.GetInstanceID());
                        _score++;
                        scoreText.text = $"{_score}";
                    }
                }
            }
        }

        private void Start()
        {
            for (var i = 0; i < pipes.Length; i++)
            {
                pipes[i].Position += new Vector3(i * PipeVelocity * PipeDelta, Random.Range(-PipeHeight, PipeHeight));
            }
        }

        private void Update()
        {
            if (_gameOver) return;
            InputStep();
            BirdStep(Time.deltaTime);
            GroundStep(Time.deltaTime);
            PipeStep(Time.deltaTime);
        }
    }
}