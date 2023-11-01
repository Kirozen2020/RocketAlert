using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Windows.Forms;
using static RocketAlert.JsonFileReader;

namespace RocketAlert
{
    public partial class SettingForm : Form
    {
        /// <summary>The regions names</summary>
        private List<string> regionsNames = new List<string>();
        /// <summary>The selected regions</summary>
        public List<string> selectedRegions;
        /// <summary>The not selected regions</summary>
        private List<string> notSelectedRegions;
        /// <summary>The initial selected</summary>
        public List<string> initialSelected;
        /// <summary>The cancel action</summary>
        public bool cancelAction = false;

        /// <summary>Initializes a new instance of the <see cref="SettingForm" /> class.</summary>
        public SettingForm()
        {
            InitializeComponent();
            this.selectedRegions = null;
        }
        /// <summary>Initializes a new instance of the <see cref="SettingForm" /> class.</summary>
        /// <param name="selected">The selected.</param>
        public SettingForm(List<string> selected)
        {
            InitializeComponent();
            this.selectedRegions = selected;
            this.initialSelected = selected.ToList();
        }

        /// <summary>Handles the Load event of the SettingForm control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void SettingForm_Load(object sender, EventArgs e)
        {
            this.cancelAction = false;
            InitListOfNames();
            if (this.selectedRegions.Count == 0)
            {
                this.selectedRegions = new List<string>();
                this.initialSelected = new List<string>();
                this.notSelectedRegions = this.regionsNames;
            }
            else
            {
                this.notSelectedRegions = this.regionsNames.Except(this.selectedRegions).ToList();
            }

            /*-----------------------------------------------------*/

            listBox1.ClearSelected();
            listBox1.Items.Clear();
            listBox1.SelectionMode = SelectionMode.MultiExtended;
            foreach(var item in this.notSelectedRegions)
            {
                listBox1.Items.Add(item.ToString());
            }

            /*-----------------------------------------------------*/

            listBox2.ClearSelected();
            listBox2.Items.Clear();
            listBox2.SelectionMode = SelectionMode.MultiExtended;
            foreach (var item in this.selectedRegions)
            {
                listBox2.Items.Add(item.ToString());
            }
        }

        /// <summary>Handles the Click event of the btnSave control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            List<string> selected = new List<string>();
            foreach(var item in listBox2.Items)
            {
                selected.Add(item.ToString());
            }
            this.cancelAction = false;
            this.selectedRegions = selected.Distinct().ToList();
            this.initialSelected = this.selectedRegions.ToList();
            this.Visible = false;
        }

        /// <summary>Initializes the list of names.</summary>
        private void InitListOfNames()
        {
            string str = "בחר הכל\r\nאבו סנאן\r\nאבו קרינאת\r\nאבו תלול\r\nאבו-גוש\r\nאבטליון\r\nאביאל\r\nאביבים\r\nאביגדור\r\nאביחיל\r\nאביעזר\r\nאבירים\r\nאבן יהודה\r\nאבן מנחם\r\nאבן ספיר\r\nאבן שמואל\r\nאבני איתן\r\nאבני חפץ\r\nאבנת\r\nאבשלום\r\nאדורה\r\nאדוריים\r\nאדמית\r\nאדרת\r\nאודים\r\nאודם\r\nאום אל פחם\r\nאום אל קוטוף\r\nאום אל-גנם\r\nאום בטין\r\nאופקים\r\nאור הגנוז\r\nאור הנר\r\nאור יהודה\r\nאור עקיבא\r\nאורה\r\nאורון תעשייה ומסחר\r\nאורות\r\nאורטל\r\nאורים\r\nאורנים\r\nאורנית\r\nאושה\r\nאזור\r\nאזור תעשייה אכזיב מילואות\r\nאזור תעשייה אלון התבור\r\nאזור תעשייה אפק ולב הארץ\r\nאזור תעשייה באר טוביה\r\nאזור תעשייה בני יהודה\r\nאזור תעשייה בר-לב\r\nאזור תעשייה בראון\r\nאזור תעשייה ברוש\r\nאזור תעשייה דימונה\r\nאזור תעשייה הדרומי אשקלון\r\nאזור תעשייה הר טוב - צרעה\r\nאזור תעשייה חבל מודיעין\r\nאזור תעשייה חצור הגלילית\r\nאזור תעשייה טירה\r\nאזור תעשייה יקנעם עילית\r\nאזור תעשייה כנות\r\nאזור תעשייה כרמיאל\r\nאזור תעשייה מבוא כרמל\r\nאזור תעשייה מבואות הגלבוע\r\nאזור תעשייה מישור אדומים\r\nאזור תעשייה מיתרים\r\nאזור תעשייה נ.ע.מ\r\nאזור תעשייה ניר עציון\r\nאזור תעשייה נשר - רמלה\r\nאזור תעשייה עד הלום\r\nאזור תעשייה עידן הנגב\r\nאזור תעשייה עמק חפר\r\nאזור תעשייה צ.ח.ר\r\nאזור תעשייה צבאים\r\nאזור תעשייה ציפורית\r\nאזור תעשייה צמח\r\nאזור תעשייה צפוני אשקלון\r\nאזור תעשייה קדמת גליל\r\nאזור תעשייה קיסריה\r\nאזור תעשייה קריית גת\r\nאזור תעשייה רגבים\r\nאזור תעשייה רותם\r\nאזור תעשייה רמת דלתון\r\nאזור תעשייה שחורת\r\nאזור תעשייה שער בנימין\r\nאזור תעשייה שער נעמן\r\nאזור תעשייה תימורים\r\nאזור תעשייה תרדיון\r\nאחווה\r\nאחוזם\r\nאחוזת ברק\r\nאחיה\r\nאחיהוד\r\nאחיטוב\r\nאחיסמך\r\nאחיעזר\r\nאיבטין\r\nאייל\r\nאיילת השחר\r\nאילון\r\nאילות\r\nאילניה\r\nאילת\r\nאירוס\r\nאיתמר\r\nאיתן\r\nאכסאל\r\nאל סייד\r\nאל עזי\r\nאל עמארני, אל מסק\r\nאל עריאן\r\nאל פורעה\r\nאל רום\r\nאל-ח'וואלד מערב\r\nאלומה\r\nאלומות\r\nאלון\r\nאלון הגליל\r\nאלון מורה\r\nאלון שבות\r\nאלוני אבא\r\nאלוני הבשן\r\nאלוני יצחק\r\nאלונים\r\nאלי עד\r\nאליאב\r\nאליכין\r\nאליפז ומכרות תמנע\r\nאליפלט\r\nאליקים\r\nאלישיב\r\nאלישמע\r\nאלמגור\r\nאלמוג\r\nאלעד\r\nאלעזר\r\nאלפי מנשה\r\nאלקוש\r\nאלקנה\r\nאמונים\r\nאמירים\r\nאמנון\r\nאמץ\r\nאמציה\r\nאניעם\r\nאעבלין\r\nאפיק\r\nאפיקים\r\nאפק\r\nאפרת\r\nארבל\r\nארגמן\r\nארז\r\nאריאל\r\nארסוף\r\nאשבול\r\nאשבל\r\nאשדוד - א,ב,ד,ה\r\nאשדוד - אזור תעשייה צפוני ונ\r\nאשדוד - ג,ו,ז\r\nאשדוד - ח,ט,י,יג,יד,טז\r\nאשדוד -יא,יב,טו,יז,מרינה,סיט\r\nאשדוד I אזור לכיש\r\nאשדות יעקב איחוד\r\nאשדות יעקב מאוחד\r\nאשחר\r\nאשכולות\r\nאשל הנשיא\r\nאשלים\r\nאשקלון - דרום\r\nאשקלון - צפון\r\nאשקלון I אזור מערב לכיש\r\nאשרת\r\nאשתאול\r\nאתר ההנצחה גולני\r\nבאקה אל גרבייה\r\nבאר אורה\r\nבאר גנים\r\nבאר טוביה\r\nבאר יעקב\r\nבאר מילכה\r\nבאר שבע - דרום\r\nבאר שבע - מזרח\r\nבאר שבע - מערב\r\nבאר שבע - צפון\r\nבאר שבע I אזור מרכז הנגב\r\nבארות יצחק\r\nבארותיים\r\nבארי\r\nבוסתן הגליל\r\nבועיינה-נוג'ידאת\r\nבוקעתא\r\nבורגתה\r\nבחן\r\nבטחה\r\nביצרון\r\nביר אלמכסור\r\nביר הדאג'\r\nביריה\r\nבית אורן\r\nבית אל\r\nבית אלעזרי\r\nבית אלפא וחפציבה\r\nבית אריה\r\nבית ברל\r\nבית ג'אן\r\nבית גוברין\r\nבית גמליאל\r\nבית דגן\r\nבית הגדי\r\nבית הלוי\r\nבית הלל\r\nבית העמק\r\nבית הערבה\r\nבית השיטה\r\nבית זית\r\nבית זרע\r\nבית חגי\r\nבית חורון\r\nבית חזון\r\nבית חלקיה\r\nבית חנן\r\nבית חנניה\r\nבית חרות\r\nבית חשמונאי\r\nבית יהושע\r\nבית יוסף\r\nבית ינאי\r\nבית יצחק - שער חפר\r\nבית ירח\r\nבית יתיר\r\nבית לחם הגלילית\r\nבית מאיר\r\nבית נחמיה\r\nבית ניר\r\nבית נקופה\r\nבית סוהר השרון\r\nבית סוהר מגידו\r\nבית סוהר נפחא\r\nבית סוהר צלמון\r\nבית סוהר קישון\r\nבית סוהר שיטה וגלבוע\r\nבית ספר אורט בנימינה\r\nבית ספר שדה מירון\r\nבית עובד\r\nבית עוזיאל\r\nבית עזרא\r\nבית עלמין תל רגב\r\nבית עריף\r\nבית צבי\r\nבית קמה\r\nבית קשת\r\nבית רימון\r\nבית שאן\r\nבית שמש\r\nבית שערים\r\nבית שקמה\r\nביתן אהרן\r\nביתר עילית\r\nבלפוריה\r\nבן זכאי\r\nבן עמי\r\nבן שמן\r\nבני ברק\r\nבני דקלים\r\nבני דרום\r\nבני דרור\r\nבני יהודה וגבעת יואב\r\nבני נצרים\r\nבני עטרות\r\nבני עי'ש\r\nבני ציון\r\nבני ראם\r\nבניה\r\nבנימינה\r\nבסמ\\\\\\\r\nבסמת טבעון\r\nבענה\r\nבצרה\r\nבצת\r\nבקוע\r\nבקעות\r\nבר גיורא\r\nבר יוחאי\r\nברוכין\r\nברור חיל\r\nברוש\r\nברחבי הארץ\r\nברטעה\r\nברכיה\r\nברעם\r\nברקאי\r\nברקן\r\nברקת\r\nבת הדר\r\nבת חן\r\nבת חפר\r\nבת עין\r\nבת שלמה\r\nבת-ים\r\nבתי מלון ים המלח\r\nג'דידה מכר\r\nג'וליס\r\nג'לג'וליה\r\nג'סר א-זרקא\r\nג'ש - גוש חלב\r\nג'ת\r\nגאולי תימן\r\nגאולים\r\nגאליה\r\nגבולות\r\nגבים, מכללת ספיר\r\nגבע בנימין\r\nגבע כרמל\r\nגבעון החדשה\r\nגבעות\r\nגבעות בר\r\nגבעות גורל\r\nגבעות עדן\r\nגבעת אבני\r\nגבעת אלה\r\nגבעת אסף\r\nגבעת ברנר\r\nגבעת הראל וגבעת הרואה\r\nגבעת השלושה\r\nגבעת וולפסון\r\nגבעת וושינגטון\r\nגבעת זאב\r\nגבעת חביבה\r\nגבעת חיים איחוד\r\nגבעת חיים מאוחד\r\nגבעת חן\r\nגבעת יערים\r\nגבעת ישעיהו\r\nגבעת כ'ח\r\nגבעת ניל'י\r\nגבעת עדה\r\nגבעת עוז\r\nגבעת שמואל\r\nגבעת שפירא\r\nגבעתי\r\nגבעתיים\r\nגברעם\r\nגבת\r\nגדות\r\nגדיש\r\nגדעונה\r\nגדרה\r\nגונן\r\nגורן\r\nגזית\r\nגזר\r\nגיאה\r\nגיבתון\r\nגיזו\r\nגילון צורית\r\nגילת\r\nגינוסר\r\nגינתון\r\nגיתה\r\nגיתית\r\nגלאון\r\nגלגל\r\nגלעד\r\nגמזו\r\nגן הדרום\r\nגן השומרון\r\nגן חיים\r\nגן יאשיה\r\nגן יבנה\r\nגן נר\r\nגן שורק\r\nגן שלמה\r\nגן שמואל\r\nגנות\r\nגנות הדר\r\nגני הדר\r\nגני טל\r\nגני יוחנן\r\nגני מודיעין\r\nגני עם\r\nגני תקווה\r\nגניגר\r\nגעש\r\nגעתון\r\nגפן\r\nגרופית\r\nגרנות הגליל\r\nגשור\r\nגשר\r\nגשר הזיו\r\nגת\r\nגת רימון\r\nדבוריה\r\nדביר\r\nדברת\r\nדגניה א\r\nדגניה ב\r\nדוב'ב\r\nדולב\r\nדור\r\nדורות\r\nדחי\r\nדימונה\r\nדיר אל-אסד\r\nדיר חנא\r\nדישון\r\nדליה\r\nדלית אל כרמל\r\nדלתון\r\nדמיידה\r\nדניאל\r\nדפנה\r\nדקל\r\nהאון\r\nהבונים\r\nהגושרים\r\nהדר עם\r\nהוד השרון\r\nהודיה\r\nהודיות\r\nהושעיה\r\nהזורע ויקנעם המושבה\r\nהזורעים\r\nהחותרים\r\nהיוגב\r\nהילה\r\nהמכללה האקדמית כנרת\r\nהמעפיל\r\nהמרכז האקדמי רופין\r\nהסוללים\r\nהעוגן\r\nהר אדר\r\nהר ברכה\r\nהר גילה\r\nהר הנגב\r\nהר עמשא\r\nהר-חלוץ\r\nהראל\r\nהרדוף\r\nהרצליה - גליל ים ומרכז\r\nהרצליה - מערב\r\nהרצליה - מרכז וגליל ים\r\nהרצליה I אזור דן\r\nהררית יחד\r\nואדי אל חמאם\r\nואדי אל נעם\r\nורד יריחו\r\nורדון\r\nזבדיאל\r\nזוהר\r\nזיקים\r\nזיתן\r\nזכרון יעקב\r\nזכריה\r\nזמר\r\nזמרת, שובה\r\nזנוח\r\nזרועה\r\nזרזיר\r\nזרחיה\r\nזרעית\r\nח'וואלד\r\nחבצלת השרון וצוקי ים\r\nחברון\r\nחג'אג'רה\r\nחגור\r\nחגלה\r\nחד נס\r\nחדיד\r\nחדרה - מזרח\r\nחדרה - מערב\r\nחדרה - מרכז\r\nחדרה - נווה חיים\r\nחדרה I אזור מנשה\r\nחוות גלעד\r\nחוות יאיר\r\nחוות עדן\r\nחוות ערנדל\r\nחוות שדה בר\r\nחוות שיקמים\r\nחולדה\r\nחולון\r\nחולית\r\nחולתה\r\nחוסן\r\nחוסנייה\r\nחופית\r\nחוקוק\r\nחורה\r\nחורפיש\r\nחורשים\r\nחזון\r\nחי-בר יטבתה\r\nחיבת ציון\r\nחיננית\r\nחיפה - כרמל ועיר תחתית\r\nחיפה - מערב\r\nחיפה - נווה שאנן ורמות כרמל\r\nחיפה - קריית חיים ושמואל\r\nחיפה I אזור המפרץ\r\nחיפה-מפרץ\r\nחירן\r\nחלמיש\r\nחלץ\r\nחמד\r\nחמדיה\r\nחמדת\r\nחמרה\r\nחמת גדר\r\nחניאל\r\nחניתה\r\nחנתון\r\nחספין\r\nחפץ חיים\r\nחפציבה ובית אלפא\r\nחצב\r\nחצבה\r\nחצור\r\nחצור הגלילית\r\nחצרים\r\nחרב לאת\r\nחרוצים\r\nחרות\r\nחריש\r\nחרמש\r\nחרשה\r\nחרשים\r\nחשמונאים\r\nטבריה\r\nטובא זנגריה\r\nטורעאן\r\nטייבה\r\nטייבה בגלבוע\r\nטירה\r\nטירת יהודה\r\nטירת כרמל\r\nטירת צבי\r\nטל מנשה\r\nטל שחר\r\nטל-אל\r\nטללים\r\nטלמון\r\nטמרה\r\nטמרה בגלבוע\r\nטנא עומרים\r\nטפחות\r\nיבול\r\nיבנאל\r\nיבנה\r\nיגור\r\nיגל\r\nיד בנימין\r\nיד השמונה\r\nיד חנה\r\nיד מרדכי\r\nיד נתן\r\nיד רמב'ם\r\nיהוד-מונוסון\r\nיהל\r\nיובלים\r\nיודפת\r\nיונתן\r\nיושיביה\r\nיזרעאל\r\nיחיעם\r\nיטבתה\r\nייט'ב\r\nיכיני\r\nינוב\r\nינוח-ג'ת\r\nינון\r\nיסוד המעלה\r\nיסודות\r\nיסעור\r\nיעד\r\nיעף\r\nיערה\r\nיערות הכרמל\r\nיפיע\r\nיפית\r\nיפעת\r\nיפתח\r\nיצהר\r\nיציץ\r\nיקום\r\nיקיר\r\nיקנעם המושבה והזורע\r\nיקנעם עילית\r\nיראון\r\nירדנה\r\nירוחם\r\nירושלים - אזור תעשייה עטרות\r\nירושלים - דרום\r\nירושלים - כפר עקב\r\nירושלים - מזרח\r\nירושלים - מערב\r\nירושלים - מרכז\r\nירושלים - צפון\r\nירושלים I אזור ירושלים\r\nירחיב\r\nירכא\r\nירקונה\r\nישובי אומן\r\nישובי יעל\r\nישעי\r\nישרש\r\nיתד\r\nכאבול\r\nכאוכב אבו אלהיג'א\r\nכברי\r\nכדורי\r\nכוכב השחר\r\nכוכב יאיר - צור יגאל\r\nכוכב יעקב\r\nכוכב מיכאל\r\nכורזים ורד הגליל\r\nכושי רמון\r\nכחל\r\nכינרת מושבה\r\nכינרת קבוצה\r\nכיסופים\r\nכיסרא סמיע\r\nכישור\r\nכלא דמון\r\nכליל\r\nכלנית\r\nכמהין\r\nכמון\r\nכנות\r\nכנף\r\nכסייפה\r\nכסלון\r\nכעביה\r\nכעביה טבאש\r\nכעביה-טבאש-חג'אג'רה\r\nכפר אביב\r\nכפר אדומים\r\nכפר אוריה\r\nכפר אחים\r\nכפר אלדד\r\nכפר ביאליק\r\nכפר ביל'ו\r\nכפר בלום\r\nכפר בן נון\r\nכפר ברא\r\nכפר ברוך\r\nכפר גדעון\r\nכפר גלים\r\nכפר גליקסון\r\nכפר גלעדי\r\nכפר גמילה מלכישוע\r\nכפר דניאל\r\nכפר האורנים\r\nכפר החורש\r\nכפר המכבי\r\nכפר הנגיד\r\nכפר הנוער ימין אורד\r\nכפר הנשיא\r\nכפר הס\r\nכפר הרא'ה\r\nכפר הרי'ף וצומת ראם\r\nכפר ויתקין\r\nכפר ורבורג\r\nכפר ורדים\r\nכפר זוהרים\r\nכפר זיתים\r\nכפר חב'ד\r\nכפר חיטים\r\nכפר חיים\r\nכפר חנניה\r\nכפר חסידים\r\nכפר חרוב\r\nכפר טבאש\r\nכפר טרומן\r\nכפר ידידיה\r\nכפר יהושע\r\nכפר יובל\r\nכפר יונה\r\nכפר יחזקאל\r\nכפר יסיף\r\nכפר יעבץ\r\nכפר כמא\r\nכפר כנא\r\nכפר מונש\r\nכפר מימון ותושיה\r\nכפר מל'ל\r\nכפר מנדא\r\nכפר מנחם\r\nכפר מסריק\r\nכפר מצר\r\nכפר מרדכי\r\nכפר נהר הירדן\r\nכפר נוער בן שמן\r\nכפר נטר\r\nכפר סאלד\r\nכפר סבא\r\nכפר סילבר\r\nכפר סירקין\r\nכפר עבודה\r\nכפר עזה\r\nכפר עציון\r\nכפר פינס\r\nכפר קאסם\r\nכפר קיש\r\nכפר קרע\r\nכפר רופין\r\nכפר רות\r\nכפר שמאי\r\nכפר שמואל\r\nכפר שמריהו\r\nכפר תבור\r\nכפר תפוח\r\nכפר תקווה\r\nכרכום\r\nכרם ביבנה\r\nכרם בן זמרה\r\nכרם בן שמן\r\nכרם מהר'ל\r\nכרם שלום\r\nכרמי יוסף\r\nכרמי צור\r\nכרמי קטיף\r\nכרמיאל\r\nכרמיה\r\nכרמים\r\nכרמית\r\nכרמל\r\nלבון\r\nלביא\r\nלבנים\r\nלהב\r\nלהבות הבשן\r\nלהבות חביבה\r\nלהבים\r\nלוד\r\nלוזית\r\nלוחמי הגטאות\r\nלוטם וחמדון\r\nלוטן\r\nלטרון\r\nלימן\r\nלכיש\r\nלפיד\r\nלפידות\r\nלקיה\r\nמאור\r\nמאיר שפיה\r\nמבוא ביתר\r\nמבוא דותן\r\nמבוא חורון\r\nמבוא חמה\r\nמבוא מודיעים\r\nמבואות יריחו\r\nמבועים\r\nמבטחים, עמיעוז, ישע\r\nמבקיעים\r\nמבשרת ציון\r\nמג'דל כרום\r\nמג'דל שמס\r\nמגדים\r\nמגדל\r\nמגדל העמק\r\nמגדל עוז\r\nמגדל תפן\r\nמגדלים\r\nמגל\r\nמגן\r\nמגן שאול\r\nמגרון\r\nמגשימים\r\nמדרך עוז\r\nמדרשת בן גוריון\r\nמודיעין\r\nמודיעין - ישפרו סנטר\r\nמודיעין - ליגד סנטר\r\nמודיעין עילית\r\nמולדת\r\nמועאוויה\r\nמועצה אזורית מבואות חרמון\r\nמועצה אזורית מרום הגליל\r\nמוצא עילית\r\nמוקיבלה\r\nמורן\r\nמורשת\r\nמזור\r\nמזכרת בתיה\r\nמזרע\r\nמזרעה\r\nמחולה\r\nמחניים\r\nמחסיה\r\nמטווח ניר עם\r\nמטולה\r\nמטע\r\nמי עמי\r\nמייסר\r\nמיצד\r\nמיצר\r\nמירב\r\nמירון\r\nמישר\r\nמיתר\r\nמכון וינגייט\r\nמכורה\r\nמכמורת\r\nמכמנים\r\nמלאה\r\nמלונות ים המלח מרכז\r\nמלכיה\r\nממשית\r\nמנוחה\r\nמנוף\r\nמנות\r\nמנחמיה\r\nמנחת מחניים\r\nמנרה\r\nמנשית זבדה\r\nמסד\r\nמסדה\r\nמסילות\r\nמסילת ציון\r\nמסלול\r\nמסעדה\r\nמע'אר\r\nמעברות\r\nמעגלים, גבעולים, מלילות\r\nמעגן\r\nמעגן מיכאל\r\nמעוז חיים\r\nמעון\r\nמעון צופיה\r\nמעונה\r\nמעיין ברוך\r\nמעיין צבי\r\nמעיליא\r\nמעלה אדומים\r\nמעלה אפרים\r\nמעלה גלבוע\r\nמעלה גמלא\r\nמעלה החמישה\r\nמעלה חבר\r\nמעלה לבונה\r\nמעלה מכמש\r\nמעלה עירון\r\nמעלה עמוס\r\nמעלה צביה\r\nמעלה רחבעם\r\nמעלה שומרון\r\nמעלות תרשיחא\r\nמענית\r\nמעש\r\nמפלסים\r\nמצדה\r\nמצובה\r\nמצוקי דרגות\r\nמצליח\r\nמצפה\r\nמצפה אבי'ב\r\nמצפה אילן\r\nמצפה יריחו\r\nמצפה נטופה\r\nמצפה רמון\r\nמצפה שלם\r\nמצר\r\nמקווה ישראל\r\nמרגליות\r\nמרום גולן\r\nמרחב עם\r\nמרחביה מושב\r\nמרחביה קיבוץ\r\nמרחצאות עין גדי\r\nמרכז אומן\r\nמרכז אזורי דרום השרון\r\nמרכז אזורי מגילות\r\nמרכז אזורי משגב\r\nמרכז חבר\r\nמרכז ימי קיסריה\r\nמרכז מיר'ב\r\nמרכז שפירא\r\nמרעית\r\nמשאבי שדה\r\nמשגב דב\r\nמשגב עם\r\nמשהד\r\nמשואה\r\nמשואות יצחק\r\nמשכיות\r\nמשמר איילון\r\nמשמר דוד\r\nמשמר הירדן\r\nמשמר הנגב\r\nמשמר העמק\r\nמשמר השבעה\r\nמשמר השרון\r\nמשמרות\r\nמשמרת\r\nמשען\r\nמתחם בני דרום\r\nמתחם פי גלילות\r\nמתחם צומת שוקת\r\nמתן\r\nמתת\r\nמתתיהו\r\nנאות גולן\r\nנאות הכיכר\r\nנאות חובב\r\nנאות מרדכי\r\nנאות סמדר\r\nנבטים\r\nנבי סמואל\r\nנגבה\r\nנגוהות\r\nנהורה\r\nנהלל\r\nנהריה\r\nנוב\r\nנוגה\r\nנוה איתן\r\nנווה\r\nנווה אור\r\nנווה אטי'ב\r\nנווה אילן\r\nנווה דניאל\r\nנווה זוהר\r\nנווה זיו\r\nנווה חריף\r\nנווה ים\r\nנווה ימין\r\nנווה ירק\r\nנווה מבטח\r\nנווה מיכאל - רוגלית\r\nנווה שלום\r\nנועם\r\nנוף איילון\r\nנוף הגליל\r\nנופי נחמיה\r\nנופי פרת\r\nנופים\r\nנופית\r\nנופך\r\nנוקדים\r\nנורדיה\r\nנורית\r\nנחושה\r\nנחל עוז\r\nנחלה\r\nנחליאל\r\nנחלים\r\nנחם\r\nנחף\r\nנחשולים\r\nנחשון\r\nנחשונים\r\nנטועה\r\nנטור\r\nנטע\r\nנטעים\r\nנטף\r\nניל'י\r\nנין\r\nניצן\r\nניצנה\r\nניצני עוז\r\nניצנים\r\nניר אליהו\r\nניר בנים\r\nניר גלים\r\nניר דוד\r\nניר ח'ן\r\nניר יפה\r\nניר יצחק\r\nניר ישראל\r\nניר משה\r\nניר עוז\r\nניר עציון\r\nניר עקיבא\r\nניר צבי\r\nנירים\r\nנירית\r\nנמרוד\r\nנס הרים\r\nנס עמים\r\nנס ציונה\r\nנעורה\r\nנעורים\r\nנעלה\r\nנעמה\r\nנען\r\nנערן\r\nנצר חזני\r\nנצר סרני\r\nנצרת\r\nנריה\r\nנשר\r\nנתיב הגדוד\r\nנתיב הל'ה\r\nנתיב העשרה\r\nנתיב השיירה\r\nנתיבות\r\nנתניה - מזרח\r\nנתניה - מערב\r\nנתניה I אזור שרון\r\nסאג'ור\r\nסאסא\r\nסביון\r\nסגולה\r\nסואעד חמירה\r\nסולם\r\nסוסיא\r\nסופה\r\nסינמה סיטי גלילות\r\nסכנין\r\nסלמה\r\nסלעית\r\nסמר\r\nסנדלה\r\nסנסנה\r\nסעד\r\nסעייה-מולדה\r\nסער\r\nספיר\r\nספסופה - כפר חושן\r\nסתריה\r\nע'ג'ר\r\nעבדון\r\nעבדת\r\nעברון\r\nעגור\r\nעדי\r\nעדי עד\r\nעדנים\r\nעוזה\r\nעוזייר\r\nעולש\r\nעומר\r\nעופר\r\nעופרים\r\nעוצם\r\nעזוז\r\nעזר\r\nעזריאל\r\nעזריה\r\nעזריקם\r\nעטרת\r\nעידן\r\nעיינות\r\nעילבון\r\nעילוט\r\nעין איילה\r\nעין אל אסד\r\nעין אל-סהלה\r\nעין בוקק\r\nעין גב\r\nעין גדי\r\nעין דור\r\nעין הבשור\r\nעין הוד\r\nעין החורש\r\nעין המפרץ\r\nעין הנצי'ב\r\nעין העמק\r\nעין השופט\r\nעין השלושה\r\nעין ורד\r\nעין זיוון\r\nעין חוד\r\nעין חצבה\r\nעין חרוד\r\nעין חרוד איחוד\r\nעין יהב\r\nעין יעקב\r\nעין כמונים\r\nעין כרמל\r\nעין מאהל\r\nעין נקובא\r\nעין עירון\r\nעין צורים\r\nעין קנייא\r\nעין ראפה\r\nעין שמר\r\nעין שריד\r\nעין תמר\r\nעינבר\r\nעינת\r\nעיר אובות\r\nעכו\r\nעכו - אזור תעשייה\r\nעלומים\r\nעלי\r\nעלי זהב\r\nעלמה\r\nעלמון\r\nעמוקה\r\nעמיחי\r\nעמינדב\r\nעמיעד\r\nעמיקם\r\nעמיר\r\nעמנואל\r\nעמקה\r\nענב\r\nעספיא\r\nעפולה\r\nעפרה\r\nעץ אפרים\r\nעצמון - שגב\r\nעראבה\r\nערב אל עראמשה\r\nערב אל-נעים\r\nערד\r\nערוגות\r\nערערה\r\nערערה בנגב\r\nעשאהל\r\nעשרת\r\nעתלית\r\nעתניאל\r\nפארן\r\nפארק תעשיות פלמחים\r\nפארק תעשייה ראם\r\nפדואל\r\nפדויים\r\nפדיה\r\nפוריה כפר עבודה\r\nפוריה נווה עובד\r\nפוריה עילית\r\nפוריידיס\r\nפורת\r\nפטיש\r\nפלך\r\nפלמחים\r\nפני קדם\r\nפנימיית עין כרם\r\nפסגות\r\nפסוטה\r\nפעמי תש'ז\r\nפצאל\r\nפקיעין\r\nפקיעין החדשה\r\nפרדס חנה-כרכור\r\nפרדסיה\r\nפרוד\r\nפרי גן\r\nפתח תקווה\r\nפתחיה\r\nצאלים\r\nצבעון\r\nצובה\r\nצוחר\r\nצוחר, אוהד\r\nצופים\r\nצופית\r\nצופר\r\nצוקים\r\nצור הדסה\r\nצור יצחק\r\nצור משה\r\nצור נתן\r\nצוריאל\r\nצורית גילון\r\nציפורי\r\nצלפון\r\nצפריה\r\nצפרירים\r\nצפת\r\nצרופה\r\nצרעה\r\nקבוצת גבע\r\nקבוצת יבנה\r\nקדומים\r\nקדימה-צורן\r\nקדיתא\r\nקדמה\r\nקדמת צבי\r\nקדר\r\nקדרון\r\nקדרים\r\nקדש ברנע\r\nקוממיות\r\nקורנית\r\nקטורה\r\nקיבוץ דן\r\nקיבוץ מגידו\r\nקידה\r\nקיסריה\r\nקלחים\r\nקליה\r\nקלנסווה\r\nקלע\r\nקציר\r\nקצר-א-סיר\r\nקצרין\r\nקצרין - אזור תעשייה\r\nקריית אונו\r\nקריית אתא\r\nקריית ביאליק\r\nקריית גת, כרמי גת\r\nקריית טבעון-בית זייד\r\nקריית ים\r\nקריית יערים\r\nקריית מוצקין\r\nקריית מלאכי\r\nקריית נטפים\r\nקריית ענבים\r\nקריית עקרון\r\nקריית שמונה\r\nקרית ארבע\r\nקרית חינוך מרחבים\r\nקרני שומרון\r\nקשת\r\nראמה\r\nראס אל-עין\r\nראס עלי\r\nראש הנקרה\r\nראש העין\r\nראש פינה\r\nראש צורים\r\nראשון לציון - מזרח\r\nראשון לציון - מערב\r\nראשון לציון I אזור השפלה\r\nרבבה\r\nרבדים\r\nרביבים\r\nרביד\r\nרגבה\r\nרגבים\r\nרהט\r\nרווחה\r\nרוויה\r\nרוחמה\r\nרומאנה\r\nרומת אל הייב\r\nרועי\r\nרותם\r\nרחוב\r\nרחובות\r\nרחלים\r\nרטורנו - גבעת שמש\r\nריחאנייה\r\nריחן\r\nריינה\r\nרימונים\r\nרינתיה\r\nרכסים\r\nרם און\r\nרמות\r\nרמות השבים\r\nרמות מאיר\r\nרמות מנשה\r\nרמות נפתלי\r\nרמלה\r\nרמת גן - מזרח\r\nרמת גן - מערב\r\nרמת גן I אזור דן\r\nרמת דוד\r\nרמת הכובש\r\nרמת הנדיב\r\nרמת השופט\r\nרמת השרון\r\nרמת יוחנן\r\nרמת ישי\r\nרמת מגשימים\r\nרמת צבי\r\nרמת רזיאל\r\nרמת רחל\r\nרנן\r\nרעים\r\nרעננה\r\nרקפת\r\nרשפון\r\nרשפים\r\nרתמים\r\nשאנטי במדבר\r\nשאר ישוב\r\nשבות רחל\r\nשבי דרום\r\nשבי ציון\r\nשבי שומרון\r\nשבלי\r\nשגב שלום\r\nשדה אברהם\r\nשדה אילן\r\nשדה אליהו\r\nשדה אליעזר\r\nשדה בוקר\r\nשדה דוד\r\nשדה ורבורג\r\nשדה יואב\r\nשדה יעקב\r\nשדה יצחק\r\nשדה משה\r\nשדה נחום\r\nשדה נחמיה\r\nשדה ניצן\r\nשדה עוזיהו\r\nשדה צבי\r\nשדות ים\r\nשדות מיכה\r\nשדי חמד\r\nשדי תרומות\r\nשדמה\r\nשדמות דבורה\r\nשדמות מחולה\r\nשדרות, איבים, ניר עם\r\nשהם\r\nשואבה\r\nשובל\r\nשומרה\r\nשומריה\r\nשומרת\r\nשוקדה\r\nשורש\r\nשורשים\r\nשושנת העמקים\r\nשזור\r\nשחר\r\nשחרות\r\nשיבולים\r\nשיטים\r\nשייח' דנון\r\nשילה\r\nשילת\r\nשכניה\r\nשלווה\r\nשלוחות\r\nשלומי\r\nשלומית\r\nשלפים\r\nשמיר\r\nשמעה\r\nשמשית\r\nשני ליבנה\r\nשניר\r\nשעב\r\nשעל\r\nשעלבים\r\nשער אפרים\r\nשער הגולן\r\nשער העמקים\r\nשער מנשה\r\nשערי תקווה\r\nשפיים\r\nשפיר\r\nשפר\r\nשפרעם\r\nשקד\r\nשקף\r\nשרונה\r\nשריגים - ליאון\r\nשריד\r\nשרשרת\r\nשתולה\r\nשתולים\r\nתארבין\r\nתאשור\r\nתדהר\r\nתובל\r\nתומר\r\nתחנת רכבת כפר יהושוע\r\nתחנת רכבת ראש העין\r\nתימורים\r\nתירוש\r\nתל אביב - דרום העיר ויפו\r\nתל אביב - מזרח\r\nתל אביב - מרכז העיר\r\nתל אביב - עבר הירקון\r\nתל אביב-יפו I אזור דן\r\nתל חי\r\nתל יוסף\r\nתל יצחק\r\nתל מונד\r\nתל עדשים\r\nתל ערד\r\nתל ציון\r\nתל קציר\r\nתל שבע\r\nתל תאומים\r\nתלם\r\nתלמי אליהו\r\nתלמי אלעזר\r\nתלמי ביל'ו\r\nתלמי יוסף\r\nתלמי יחיאל\r\nתלמי יפה\r\nתלמים\r\nתמרת\r\nתנובות\r\nתעוז\r\nתעשיון חצב\r\nתעשיון צריפין\r\nתפרח\r\nתקומה\r\nתקומה וחוות יזרעם\r\nתקוע\r\nתרום\r\n";
            this.regionsNames = str.Split(new[] { "\r\n" }, StringSplitOptions.None).ToList();
        }

        /// <summary>Filters the specified filter.</summary>
        /// <param name="filter">The filter.</param>
        /// <param name="value">The value.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public List<string> Filter(string filter, List<string> value)
        {
            List<string> strings = new List<string>();
            foreach(string item in value)
            {
                if (item.Contains(filter))
                {
                    strings.Add(item);
                }
            }
            return strings;
        }

        /// <summary>Handles the Click event of the btnCancel control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.cancelAction = true;
            this.selectedRegions = this.initialSelected.ToList();
            this.Visible = false;
        }

        /// <summary>Handles the Click event of the btnSelect control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void btnSelect_Click(object sender, EventArgs e)
        {
            List<object> selectedItems = new List<object>();
            foreach(var item in listBox1.SelectedItems)
            {
                selectedItems.Add(item);
            }
            foreach(var item in selectedItems)
            {
                listBox1.Items.Remove(item.ToString());
                this.notSelectedRegions.Remove(item.ToString());
                this.selectedRegions.Add(item.ToString());
            }
            this.selectedRegions.Sort();
            foreach(var item in this.selectedRegions)
            {
                if (!listBox2.Items.Contains(item.ToString()))
                {
                    listBox2.Items.Add(item.ToString());
                }
            }
        }

        /// <summary>Handles the Click event of the btnUnselect control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void btnUnselect_Click(object sender, EventArgs e)
        {
            List<object> selectedItems = new List<object>();
            foreach (var item in listBox2.SelectedItems)
            {
                selectedItems.Add(item);
            }
            foreach (var item in selectedItems)
            {
                listBox2.Items.Remove(item.ToString());
                this.selectedRegions.Remove(item.ToString());
                this.notSelectedRegions.Add(item.ToString());
            }
            this.notSelectedRegions.Sort();
            foreach(var item in this.notSelectedRegions)
            {
                if (!listBox1.Items.Contains(item.ToString()))
                {
                    listBox1.Items.Add(item.ToString());
                }
            }
        }

        /// <summary>Handles the TextChanged event of the tbSearchSelected control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void tbSearchSelected_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            List<string> names = Filter(textBox.Text, this.selectedRegions);

            if (names != null)
            {
                listBox2.ClearSelected();
                listBox2.Items.Clear();
                foreach (string name in names)
                {
                    listBox2.Items.Add(name);
                }
            }
        }

        /// <summary>Handles the TextChanged event of the tbSearchNotSelected control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void tbSearchNotSelected_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            List<string> names = Filter(textBox.Text, this.notSelectedRegions);

            if (names != null)
            {
                listBox1.ClearSelected();
                listBox1.Items.Clear();
                foreach (string name in names)
                {
                    listBox1.Items.Add(name);
                }
            }
        }

        /// <summary>Handles the FormClosing event of the SettingForm control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FormClosingEventArgs" /> instance containing the event data.</param>
        private void SettingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.cancelAction = true;
            this.selectedRegions = this.initialSelected;
            this.Visible = false;
        }
    }
}
