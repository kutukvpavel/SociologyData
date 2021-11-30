# SociologyData

Графики статистики ДТП в России (на основе выгрузки из stats.gibdd.ru и других открытых источников) для социологического исследования.

На официальном сайте ГИБДД уже несколко месяцев ведутся технические работы, из-за которых не работает инфографика за периоды 200х-2020, к тому же нельзя выгрузить статистику за 2015-2020 годы. Нами собраны данные по недостающему периоду (в субъектах: РФ, Москва, Санкт-Петербург, Республика Татарстан) из других открытых источников, в том числе https://github.com/Shorstko/GibddStat (см. в релизе), и написан простейший просмоторщик.

TODO: ComboBox для колонки в таблице (пока что по умолчанию строится вторая колонка, то есть число ДТП по выбранному показателю).

Requirements: .NET 5.0.

Uses: 
 - https://scottplot.net/
 - http://avaloniaui.net/
 - https://github.com/closedxml/closedxml
 - https://github.com/ExcelDataReader/ExcelDataReader
 - https://github.com/morelinq/MoreLINQ
 - https://www.newtonsoft.com/json
 
