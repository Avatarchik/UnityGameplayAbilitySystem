﻿/*
 * Created on Mon Nov 04 2019
 *
 * The MIT License (MIT)
 * Copyright (c) 2019 Sahil Jain
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software
 * and associated documentation files (the "Software"), to deal in the Software without restriction,
 * including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense,
 * and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so,
 * subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all copies or substantial
 * portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
 * TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
 * THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
 * TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */

using GameplayAbilitySystem.GameplayEffects.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace GameplayAbilitySystem.GameplayEffects.Systems {

    [UpdateInGroup(typeof(GameplayEffectGroupUpdateBeginSystem))]
    public class GameplayEffectDurationUpdateSystem : JobComponentSystem {
        [BurstCompile]
        struct GameplayEffectDurationUpdateSystemJob : IJobForEach<GameplayEffectDurationComponent> {
            public float deltaTime;
            public void Execute(ref GameplayEffectDurationComponent duration) {
                duration.RemainingTime -= deltaTime;
                duration.RemainingTime = math.max(0, duration.RemainingTime);
            }
        }

        protected override JobHandle OnUpdate(JobHandle inputDependencies) {
            var job = new GameplayEffectDurationUpdateSystemJob
            {
                deltaTime = Time.deltaTime
            };

            // Now that the job is set up, schedule it to be run. 
            return job.Schedule(this, inputDependencies);
        }
    }
}