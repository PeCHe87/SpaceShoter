﻿using System;
using UnityEngine;

public class Player : CharacterEntity
{
    [SerializeField] private ScriptablePlayer _playerData = null;

    private Weapon _weapon = null;

    private void Awake()
    {
        Health = GetComponent<HealthController>();
        Health.SetMaxHealth(_playerData.MaxHealth);

        _weapon = GetComponent<Weapon>();
    }

    private void Update()
    {
        if (_weapon != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _weapon.Fire();
            }
        }
    }
}