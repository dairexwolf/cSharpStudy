 // Элементы для работы с датами представлены следующими классами: Calendar и DatePicker.

// Calendar представляет собой элемент в виде календаря, тогда как DatePicker - текстовое поле для ввода даты с выпадающим календарем после ввода.

/*
Они имеют некоторые общие свойства:

* BlackoutDates - Принимает в качестве значения объект CalendarDateRange, задающий с помощью свойств Start и End диапазон дат, которые будут зачеркнуты в календаре.

* DisplayDateStart и DisplayDateEnd - Задают соответственно начальную и конечную дату диапазона, который будет отображаться в календаре.

* IsTodayHighlighted - Отмечает, будет ли выделена текущая дата

* SelectedDate(SelectedDates) - Задает выделенную дату (диапазон выделенных дат)

* FirstDayOfWeek - Задает первый день недели
*/

// Также Calendar имеет еще два важных свойства: DisplayMode(формат отображения дат) и SelectionMode (способ выделения).

/*
DisplayMode может принимать одно из следующих значений:

* Month (по умолчанию) отображает все дни текущего месяца

* Decade отображает все года текущего десятилетия

* Year отображает все месяцы текущего года
*/

/*
SelectionMode может принимать одно из следующих значений:

* SingleDate (по умолчанию) выделяет только одну дату

* None запрещает выделение

* SingleRange по нажатию на Ctrl выделяет несколько последовательно идущих дат

* MultipleRange по нажатию на Ctrl выделяет несколько не последовательно идущих диапазонов дат

*/

// Пример
<Calendar x:Name="calendar1" FirstDayOfWeek="Monday"
        SelectedDatesChanged="calendar_SelectedDatesChanged">
    <Calendar.BlackoutDates>
        <CalendarDateRange Start="7/2/2022" End="20/8/2022"></CalendarDateRange>
    </Calendar.BlackoutDates>
</Calendar>

// Обратите внимание, что при задании даты мы сначала пишем месяц, а потом число.

// Чтобы использовать в программе выбор даты пользователем, мы можем обработать событие SelectedDatesChanged в коде c#:
private void calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
{
    DateTime? selectedDate = calendar1.SelectedDate;	// ? - значит может иметь значение null
 
    MessageBox.Show(selectedDate.Value.Date.ToShortDateString());
}

// Для создания набора выделенных дат нам надо подключить в xaml пространство имен System:
<Calendar x:Name="calendar1" FirstDayOfWeek="Monday" SelectionMode="MultipleRange"
        xmlns:sys="clr-namespace:System;assembly=mscorlib">
    <Calendar.SelectedDates>
        <sys:DateTime>2/14/2022</sys:DateTime>
            <sys:DateTime>2/15/2022</sys:DateTime>
            <sys:DateTime>2/18/2022</sys:DateTime>
    </Calendar.SelectedDates>
</Calendar>





















