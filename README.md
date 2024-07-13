Написать симуляцию игры:

Есть игровое поле размером N на N, на нём есть M животных (M <= N * N / 2) и M еды. 
Каждое из животных ест только свою еду, остальную игнорирует. Чужая еда не является препятствием для перемещения, однако другие животные должны мешать перемещению (колизиться).

При появлении животного одновременно на карте появляется еда для него в случайной свободной точке поля (не должна наслаиваться на существующую еду, но может наслаиваться на животных которые эту еду не едят), на расстоянии не большем чем животное может пройти за 5 секунд.

Животное идёт к своей еде со скоростью V, алгоритм поиска пути выбрать самостоятельно (никаких ускорений, юнит моментально умеет менять направление, и движется равномерно). Когда оно с ней колизится — еда исчезает, играется визуальный эффект (любая партикл система время жизни 1 секунда), и появляется новая, по тем же правилам. И так до бесконечности.

При создании новой симуляции  — появляется менюшка с ползунками для
N (2…1000)
M (1.N * N / 2)
V (1…100) юнитов в секунду

Во время симуляции должен быть ползунок, скорости симуляции (по дефолту 1,0…1000), 0 = пауза.

В качестве животных и еды, подойдут обычные кубы. Необходимо реализовать 3Д версию.

Хорошим дополнением будет сделать кнопку сохранить текущее состояние симуляции — а при старте приложения должно быть две кнопки — начать новую симуляцию или продолжить старую;
