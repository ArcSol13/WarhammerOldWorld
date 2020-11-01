using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Localization;

namespace WarhammerOldWorld.QuestManagment
{
    public abstract class QuestAction
    {
        /// <summary>
        /// Observes the completion state of this Action
        /// </summary>
        /// <returns></returns>
        public IObservable<bool> OnComplete() => onComplete;
        /// <summary>
        /// Observes the fail state of the Action
        /// </summary>
        /// <returns></returns>
        public IObservable<bool> OnFail() => onFail;
        /// <summary>
        /// Returns a observable to the log subject, which returns a Text Object every
        /// time the action has something to log
        /// </summary>
        public IObservable<TextObject> logUpdated => logSubject;
        protected QuestAction()
        {
            Init();
        }
        protected BehaviorSubject<bool> onComplete;
        protected BehaviorSubject<bool> onFail;
        protected Subject<TextObject> logSubject;
        public abstract IObservable<int> UpdateScore();
        #region logging
        /// <summary>
        /// Task name that will apear in the Journal Log
        /// </summary>
        /// <returns></returns>
        public abstract TextObject TaskName();
        /// <summary>
        /// Task description that will apear in the Journal Log
        /// </summary>
        /// <returns></returns>
        public abstract TextObject TaskDescription();

        /// <summary>
        /// Start progress that will apear in the log
        /// </summary>
        /// <returns></returns>
        public abstract int StartProgress();
        /// <summary>
        /// Max value of the progress in the journal log
        /// </summary>
        /// <returns></returns>
        public abstract int Target();
        #endregion
        ~QuestAction()
        {
            logSubject?.Dispose();
            onComplete?.Dispose();
            onFail?.Dispose();
        }

        /// <summary>
        /// Inits the class, should be called once on constructor, once on game load. 
        /// </summary>
        public virtual void Init()
        {
            onComplete = new BehaviorSubject<bool>(false);
            onFail = new BehaviorSubject<bool>(false);
            logSubject = new Subject<TextObject>();
        }
    }
}
