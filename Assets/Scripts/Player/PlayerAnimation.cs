using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private PlayerInput _input;

    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _input = GetComponent<PlayerInput>();

        var movement = GetComponent<PlayerMovement>();
        var shooting = GetComponent<PlayerShooting>();
        
        movement.OnJump += OnJump;
        PlayerShooting.OnShoot += OnShoot;
    }

    private void OnShoot(Vector3 s)
    {
        _animator.SetTrigger("shoot");
    }

    private void OnJump(object sender, EventArgs eventArgs)
    {
        _animator.SetTrigger("jump");
    }

    void Update()
    {
        float blend = 0;
        if (_input.AimingUp && _input.MoveDirection != 0) blend = 0.5f;
        else if (_input.AimingUp) blend = 1;

        transform.localScale = new Vector3(_input.LastFacingDirection, 1, 1);

        if (_input.InterruptMoveKey)
        {
            _animator.SetBool("walking", false);
        }
        else
            _animator.SetBool("walking", _input.MoveDirection != 0);

        _animator.SetFloat("shootdir", blend);
    }
}
