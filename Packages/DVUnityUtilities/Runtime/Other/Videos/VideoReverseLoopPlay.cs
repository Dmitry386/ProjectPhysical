using System.Collections;
using UnityEngine;
using UnityEngine.Video;

namespace Packages.DVUnityUtilities.Runtime.Other.Videos
{
    internal class VideoReverseLoopPlay : MonoBehaviour
    {
        [SerializeField] private VideoPlayer _videoPlayer;
        private bool _reverse;
        private bool seekDone;

        private void Awake()
        {
            _videoPlayer.seekCompleted += seekCompleted;
        }

        void seekCompleted(VideoPlayer par)
        {
            StartCoroutine(WaitToUpdateRenderTextureBeforeEndingSeek());
        }

        public void UpdateVideoPlayerToFrame(long frame)
        {
            //If you are currently seeking there is no point to seek again.
            if (!seekDone)
                return;

            // You should pause while you seek for better stability
            _videoPlayer.Pause();

            _videoPlayer.frame = frame;
            seekDone = false;
        }

        IEnumerator WaitToUpdateRenderTextureBeforeEndingSeek()
        {
            yield return new WaitForEndOfFrame();
            seekDone = true;
        }

        void Update()
        {
           // _videoPlayer.playbackSpeed = -1;

            //if (_reverse == true)
            //{
            //    _videoPlayer.frame--; // can get fancy here if you want, this is super basic
            //}

            //if (_videoPlayer.frame <= 0)
            //{
            //    _reverse = false;
            //}

            //if (_videoPlayer.frame >= 0)
            //{
            //  //  _videoPlayer.Pause();
            //    _videoPlayer.frame = Mathf.RoundToInt(Mathf.PingPong(Time.time * _videoPlayer.frameCount, _videoPlayer.frameCount));
            //    //Debug.Log(video.frame);
            //}

            if (!_reverse)
            {
                UpdateVideoPlayerToFrame(_videoPlayer.frame+1);
                if (_videoPlayer.frame >= (long)_videoPlayer.frameCount)
                {
                    _reverse = true;
                }
            }
            else
            {
                UpdateVideoPlayerToFrame(_videoPlayer.frame - 1);
                if (_videoPlayer.frame <= 0)
                {
                    _reverse = false;
                }
            }
        }

        //private void Awake()
        //{
        //    //     _videoPlayer.playbackSpeed = 0;
        //}

        //private void Update()
        //{
        //    double time = _videoPlayer.time;
        //    Debug.Log(_videoPlayer.canSetTime);
        //    if (!_reverse)
        //    {
        //        time += 1;
        //        SetClipWithTime(time);
        //        if (time >= _videoPlayer.length)
        //        {
        //            _reverse = true;
        //        }
        //    }
        //    else
        //    {
        //        time -= 1;
        //        SetClipWithTime(time);
        //        if (time <= 0)
        //        {
        //            _reverse = false;
        //        }
        //    }

        //}

        //public void SetClipWithTime(double time)
        //{
        //    StartCoroutine(SetTimeRoutine(time));
        //}

        //IEnumerator SetTimeRoutine(double time)
        //{
        //    if (_videoPlayer.isPlaying)
        //    {
        //        _videoPlayer.Stop();
        //    }

        //    _videoPlayer.Prepare();
        //    yield return new WaitUntil(() => _videoPlayer.isPrepared);
        //    yield return new WaitUntil(() => _videoPlayer.canSetTime);

        //    _videoPlayer.Play();
        //    _videoPlayer.time = time;
        //}

        private void OnValidate()
        {
            _videoPlayer ??= GetComponent<VideoPlayer>();
        }
    }
}