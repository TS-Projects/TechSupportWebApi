namespace TechSupport.Common
{
    public class GlobalConstants
    {
        public const string DefaultRole = "User";
        public const string AdminRole = "Administrator";
        public const string ModeratorRole = "Moderator";

        // Enums
        public const string Male = "Мъж";

        public const string Female = "Жена";
        public const string Planned = "Планов";
        public const string Emergency = "Спешен";
        public const string Professor = "професор";
        public const string AssociateProfessor = "доцент";
        public const string AssitantProfessor = "асистент";
        public const string Md = "д-р";
        public const string Ecg = "ЕКГ";
        public const string Xray = "Рентген";
        public const string Holter = "Холтер";
        public const string Scanner = "Скенер";
        public const string Mammography = "Мамография";
        public const string Other = "Друго";

        // Controllers
        public const string ProjectTitle = "Health Consult";

        public const string AdministrationControllerTitle = "Администрация";
        public const string HospitalsControllerTitle = "Болници";
        public const string SpecialistsControllerTitle = "Специалисти";
        public const string SpecialtiesControllerTitle = "Специалности";
        public const string SchedulesControllerTitle = "График";
        public const string LogsControllerTitle = "Системна хронология";
        public const string ConsultationsControllerTitle = "Консултации";
        public const string NewConsultationsControllerTitle = "Нова";
        public const string SentConsultationsControllerTitle = "Изпратени";
        public const string RecievedConsultationsControllerTitle = "Получени";
        public const string ReportsControllerTitle = "Отчети";

        // User
        public const string Login = "Вход";

        public const string Register = "Регистрация";
        public const string Logoff = "Изход";
        public const string Hello = "Здравей ";
        public const string Manage = "Управление на акаунта";
        public const string Resetpassword = "Рестартиране на парола";

        // Grid
        public const string Send = "Изпрати";

        public const string EditLabel = "Редакция";
        public const string AddEditLabel = "Добави/Редактирай";
        public const string Create = "Нов запис";
        public const string Update = "Обнови";
        public const string Delete = "Изтрий";
        public const string Cancel = "Отказ";
        public const string GroupMessage = "Провлачете заглавие на колона тук, за да групирате по нея";
        public const string GetLocation = "Локация";

        // Scheduler
        public const string Specialist = "Специалист";

        public const string TimeZone = "Etc/GMT+2";

        // ViewModels

        // Hospital
        public const string NameDisplay = "Име";

        public const string NameRequireText = "Името е задължително";
        public const string AddressDisplay = "Адрес";
        public const string PhoneDisplay = "Телефон";

        // Log
        public const string DateDisplay = "Дата";

        public const string DateRequireText = "Датата е задължителна";
        public const string UserDisplay = "Потребител";
        public const string ActionDisplay = "Действие";
        public const string InformationDisplay = "Информация";

        // Schedule
        public const string SpecialistDisplay = "Специалист";

        public const string SpecialistRequireText = "Специалистът е задължителен";
        public const string TitleDisplay = "Заглавие";
        public const string DescriptionDisplay = "Описание";
        public const string StartDisplay = "Начало";
        public const string StartRequireText = "Началния час е задължителен";
        public const string EndDisplay = "Край";
        public const string EndRequireText = "Крайния час е задължителен";

        // Specialist
        public const string NamesDisplay = "Имена";

        public const string NamesRequireText = "Имената са задължителни";
        public const string SpecialistTitleDisplay = "Титла";
        public const string SpecialityDisplay = "Специалност";
        public const string SpecialityRequireText = "Специалността е задължителна";
        public const string HospitalDisplay = "Болница";
        public const string HospitalRequireText = "Болницата е задължителна";
        public const string UserRequireText = "Потребителското име е задължително";

        // ActionLinks
        public const string View = "Разгледай";
    }
}