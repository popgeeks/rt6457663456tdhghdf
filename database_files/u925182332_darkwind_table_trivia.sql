
-- --------------------------------------------------------

--
-- Table structure for table `trivia`
--

CREATE TABLE `trivia` (
  `Question_Key` int(10) UNSIGNED NOT NULL,
  `Question` varchar(300) NOT NULL DEFAULT '',
  `Answer` varchar(100) NOT NULL DEFAULT '',
  `False_Answer` varchar(100) NOT NULL DEFAULT '',
  `Points` int(10) UNSIGNED NOT NULL DEFAULT 0,
  `Group_Key` varchar(45) NOT NULL DEFAULT '',
  `Owner` varchar(50) NOT NULL DEFAULT 'darklumina'
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- Dumping data for table `trivia`
--

INSERT INTO `trivia` (`Question_Key`, `Question`, `Answer`, `False_Answer`, `Points`, `Group_Key`, `Owner`) VALUES
(1, 'Time is associated with what?', 'Feelings', 'A Fake Watch', 5, 'masamune', 'darklumina'),
(2, 'There are how many seconds in a day?', '86400', '42', 2, 'masamune', 'darklumina'),
(3, 'Squall has who on his ring?', 'Griever', 'A Naked Image of Sephiroth', 3, 'masamune', 'darklumina'),
(4, 'Seifer Almasy\'s maiden name?', 'Almasy', 'PantyBox', 2, 'masamune', 'darklumina'),
(5, 'In FF7, Jenova married Cloud and had tons of babies, one a mechanical monster at which they named Sephiroth.  True or false?', 'False', 'You bet!', 1, 'masamune', 'darklumina'),
(6, 'Aeris initially looks at Cloud and is reminded of who?', 'Zack', 'J102Y in Pink Panties', 3, 'masamune', 'darklumina'),
(7, 'Laguana\'s sweetie who had his life taken away from him?', 'Raine', 'Polom', 2, 'masamune', 'darklumina'),
(8, 'What is the car that Final Fantasy 8 is associated with?  Honda ____', 'Echo', 'Intrepid', 3, 'masamune', 'darklumina'),
(9, 'Cloud and Tifa sleep beside a what when waiting for the others to decide if they will fight Sephiroth?  a ____', 'Rock', 'A Mobile Inn', 3, 'masamune', 'darklumina'),
(10, 'Who is the tallest main character in Final Fantasy 7?', 'Barrett', 'RedXIII', 3, 'masamune', 'darklumina'),
(11, 'How old is Aeris if you decide to name your son bug zapper, multiply her age by 2, then name your daughter \"das strassenbahn\"?', '44', '138 Years Old', 5, 'masamune', 'darklumina'),
(12, 'Nanaki\'s father did what after Nanaki saw him?', 'Cried', 'Crapped Himself', 2, 'masamune', 'darklumina'),
(13, 'What is the name of the traditional FF8 Triple Triad background song?', 'Shuffle and Boogie', 'The Tennessee Waltz', 1, 'masamune', 'darklumina'),
(14, 'How many plush pikachus does darklumina have?', '13', 'Too many', 2, 'general', 'darklumina'),
(15, 'How many licks does it take to get to the center of a tootsie pop?', '3', 'Lick me and find out', 1, 'general', 'darklumina'),
(16, 'Darklumina lives in what USA state?', 'Tennessee', 'Antarctica', 2, 'general', 'darklumina'),
(17, 'How many ounces are in a standard US soda can?', '12', '38 grams', 1, 'general', 'darklumina'),
(18, 'What is water from the sky called?', 'rain', 'Bird pee', 1, 'general', 'darklumina'),
(19, 'How many gumballs come out of a standard gumball machine?', '1', '13', 1, 'general', 'darklumina'),
(20, 'How many yards are on a football field?', '120', '95', 2, 'general', 'darklumina'),
(21, 'What is the next engineering notation (ie. Mega, Giga) after a Gigabyte?', 'Terra', 'SuperDuper', 1, 'general', 'darklumina'),
(22, 'How many days are in a typical year?', '365.25', '400', 3, 'general', 'darklumina'),
(23, 'Cows fart what type of hazardous gas which contributes to 80% of ozone depletion?', 'Methane', 'As if I smell cows butts....', 5, 'general', 'darklumina'),
(24, 'What is the heaviest element in the compound H2O?', 'Oxygen', 'The 2', 2, 'general', 'darklumina'),
(25, 'If a chicken crossed the road at 12:01pm and a car left his house 30 miles away at 11:45pm, on average, what is the biggest grocery shopping holiday of the year?', 'Thanksgiving', '4:00 PM', 2, 'general', 'darklumina'),
(26, 'January and February are prevalent for what type of virus?', 'Flu', 'Herpes', 2, 'general', 'darklumina'),
(27, 'According to old Tandy XL 1000 computers, 0^0 = ?', '1', 'Massive Error', 4, 'general', 'darklumina'),
(28, 'In FF4, Cecil\'s last name is?', 'Harvey', 'Pupenmeyer', 3, 'venja', 'darklumina'),
(29, 'Leviathon was originally used with what card in a no cc\'s no dupes match?', 'Diablos', 'The Legendary Atomsplitter', 3, 'venja', 'darklumina'),
(30, 'Aeris is killed by what sword?', 'Masamune', 'Ultimate Weapon', 2, 'venja', 'darklumina'),
(31, 'In FF1, what does admantoise get you when you bring it to dwarve cave?', 'Excalibur', 'Mega Potion', 4, 'venja', 'darklumina'),
(32, 'Final Fantasy was orignally scheduled to be squares _________ game.', 'last', 'Most Profitable', 3, 'venja', 'darklumina'),
(33, 'Who is the Main Girl character in Final Fantasy 3 (US, FF6 JAP)', 'Terra', 'Janet Reno', 2, 'venja', 'darklumina'),
(34, 'What command is used to make your name appear in green writing?', '/me', 'Alt-F4', 1, 'venja', 'darklumina'),
(35, 'The last boss in FF1 is?', 'Chaos', 'President Mahmoud Ahmadinejad', 3, 'venja', 'darklumina'),
(36, 'In FF7, what is yuffies lvl 3-2 limit break?', 'Doom of the Living', 'Panty Power', 2, 'laser', 'darklumina'),
(37, 'In FF8, if you beat Omega, you get the Cloud card.  True or false?', 'false', 'Oh... sorry... wrong answer.', 1, 'laser', 'darklumina'),
(38, 'In FF8, who is the final boss?', 'Ultimecia', 'A Naked Rinoa', 2, 'laser', 'darklumina'),
(39, 'In FF8, who sings the song \"eyes on me\"?', 'Faye Wong', 'Spice Girls', 1, 'laser', 'darklumina'),
(40, 'In FF7, how much hp does midgar zolem have?', '4000', '99999', 2, 'laser', 'darklumina'),
(41, 'In FF7, how many dates can you go on in the golden saucer?', '4', '2 if you wear a condom', 3, 'laser', 'darklumina'),
(42, 'In FF8, what does the GF Tonberry say when he uses his attack?', 'Doink', 'Kupo Wark', 2, 'laser', 'darklumina'),
(43, 'in FF7, What can u find in tifa\'s cabinet when cloud has his first flash back in nibelhiem?', 'othropedic underwear', 'A diary with sexual thoughts of darklumina', 3, 'laser', 'darklumina'),
(44, 'In FF3/6, Who is the first magicide u get?', 'Rumah', 'Super Mover', 2, 'laser', 'darklumina'),
(45, 'What is Cloud\'s Favorite number?', '7', '777', 2, 'laser', 'darklumina'),
(46, 'In FF8, what is Ultima Weapon\'s one hit kill attack called?', 'Light Pillar', 'Doom', 3, 'laser', 'darklumina'),
(47, 'What is Tidus\'s Final Overdrive in FFX?', 'Gaydar', 'Blitz Ace', 2, 'FFX', 'Ultimacloud'),
(48, 'What is the name of the kid who gives you the Choco Lure Materia In \r\nFF7?', 'Spanky', 'Billy', 1, 'FFX', 'Ultimacloud'),
(49, 'In FF4 Whats the Item you Steal to Make the Final Boss Easier?', 'Pussywillow of Doom', 'Dark Matter', 3, 'FF2', 'Ultimacloud'),
(50, 'In FFX how many Jecht Spheres are there not including the Braska \r\nSphere?', 'What\'s a Jecht Sphere?', '9', 2, 'FFX', 'Ultimacloud'),
(51, 'Who is the female summoner that challenges Yuna to one on one Aeon \r\nduels throughout the game?', 'Tidus in Drag', 'Belgemine', 2, 'FFX', 'Ultimacloud'),
(52, 'When You Fight Seymour on the HighBridge was is the name of Seymours \r\npartner in that batlle?', 'Evil Spirit', 'Mortibody', 2, 'FFX', 'Ultimacloud'),
(53, 'In Final Fantasy Tactics What is the Main Characters Childhood Best \r\nfriends Name?', 'Ramza', 'Delita', 2, 'FFT', 'Ultimacloud'),
(54, 'What is the Engineers name that joins your party inn FFT?', 'Cid', 'Mustadio', 2, 'FFT', 'Ultimacloud'),
(55, 'What is Orlandus Nickname?', 'Big Sparky', 'Thunder God', 3, 'FFT', 'Ultimacloud'),
(56, 'In FFT what is the only Chocobo That can Fly?', 'Boko', 'Black Chocobos', 3, 'FFT', 'Ultimacloud'),
(57, 'What is Aeris\'s Mothers name?', 'Francheca Dawn Buttafuco III', 'Ifalna', 4, 'FF7', 'Ultimacloud'),
(58, 'Whats the name of Tifa\'s Martial Arts Trainer?', 'Raine', 'Zangan', 3, 'FF7', 'Ultimacloud'),
(59, 'What Enemy do you defeat to get Clouds Ultimate Weapon?', 'Aeris in Crack form', 'Ultima Weapon', 1, 'FF7', 'Ultimacloud'),
(60, 'What world leadership organization is as useless as the hanging ball at the back of your throat?', 'United Nations', 'Fathers Against Rationalizing Tetanus Shots (FARTS)', 2, 'general', 'darklumina'),
(61, 'Which hostile middle eastern country is formally known as the Persian Empire?', 'Iran', 'Sudan', 3, 'general', 'darklumina');
