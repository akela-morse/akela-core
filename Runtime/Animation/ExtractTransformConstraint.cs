﻿#if AKELA_ANIMATION_RIGGING
using System;
using Unity.Burst;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Animations.Rigging;

namespace Akela.Animation
{
    [BurstCompile]
    public struct ExtractTransformConstraintJob : IWeightedAnimationJob
    {
        public ReadWriteTransformHandle bone;

        public FloatProperty jobWeight { get; set; }

        public Vector3Property position;
        public Vector4Property rotation;

        public void ProcessRootMotion(AnimationStream stream) { }

        public void ProcessAnimation(AnimationStream stream)
        {
            AnimationRuntimeUtils.PassThrough(stream, bone);

            var pos = bone.GetPosition(stream);
            var rot = bone.GetRotation(stream);

            position.Set(stream, pos);
            rotation.Set(stream, new Vector4(rot.x, rot.y, rot.z, rot.w));
        }
    }

    [Serializable]
    public struct ExtractTransformConstraintData : IAnimationJobData
    {
        [SyncSceneToStream] public Transform bone;

        public Vector3 position;
        public Quaternion rotation;

        public bool IsValid()
        {
            return bone;
        }

        public void SetDefaultValues()
        {
            bone = null;

            position = Vector3.zero;
            rotation = Quaternion.identity;
        }
    }

    public class ExtractTransformConstraintJobBinder : AnimationJobBinder<ExtractTransformConstraintJob, ExtractTransformConstraintData>
    {
        public override ExtractTransformConstraintJob Create(Animator animator, ref ExtractTransformConstraintData data, Component component)
        {
            return new ExtractTransformConstraintJob
            {
                bone = ReadWriteTransformHandle.Bind(animator, data.bone),
                position = Vector3Property.Bind(animator, component, "m_Data." + nameof(data.position)),
                rotation = Vector4Property.Bind(animator, component, "m_Data." + nameof(data.rotation))
            };
        }

        public override void Destroy(ExtractTransformConstraintJob job) { }
    }

    [AddComponentMenu("Animation Rigging/Extract Transform Constraint"), DisallowMultipleComponent]
    public class ExtractTransformConstraint : RigConstraint<ExtractTransformConstraintJob, ExtractTransformConstraintData, ExtractTransformConstraintJobBinder> { }
}
#endif