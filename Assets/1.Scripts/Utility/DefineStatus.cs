using UnityEngine;

namespace DefineStatus
{

    public struct BaseStatus
    {
        public int _maxHp;
        public int _currentHp;
        public float _armor;
    }

    public struct CharacterStatus
    {
        public float _attackCoolTime;
        public float _currentCoolTime;
        public float _damage;
        public float _range;
    }

}
