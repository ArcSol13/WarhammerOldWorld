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
        public IObservable<bool> OnComplete() => onComplete;

        public IObservable<bool> OnFail() => onFail;
        public IObservable<TextObject> logUpdated => logSubject;
        protected QuestAction()
        {
        }
        protected BehaviorSubject<bool> onComplete = new BehaviorSubject<bool>(false);
        protected BehaviorSubject<bool> onFail = new BehaviorSubject<bool>(false);
        protected Subject<TextObject> logSubject = new Subject<TextObject>();
        public abstract IObservable<int> UpdateScore();
        #region logging
        public abstract TextObject TaskName();
        public abstract TextObject TaskDescription();
        public abstract int StartProgress();
        public abstract int Target();
        #endregion
        ~QuestAction()
        {
            logSubject?.Dispose();
            onComplete?.Dispose();
            onFail?.Dispose();
        }

        public abstract void Init();
    }
}
