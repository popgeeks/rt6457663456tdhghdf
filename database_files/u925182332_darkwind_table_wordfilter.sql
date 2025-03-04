
-- --------------------------------------------------------

--
-- Table structure for table `wordfilter`
--

CREATE TABLE `wordfilter` (
  `id` int(10) UNSIGNED NOT NULL,
  `word` varchar(45) NOT NULL DEFAULT '',
  `replace` varchar(45) NOT NULL DEFAULT ''
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- Dumping data for table `wordfilter`
--

INSERT INTO `wordfilter` (`id`, `word`, `replace`) VALUES
(1, 'bitch', '*****'),
(2, 'fuck', '****'),
(3, 'dick', '****'),
(4, 'pussy', '*****'),
(5, 'bastard', 'b***tard'),
(6, 'goddamn', 'goshdarn'),
(7, 'god damn', 'goshdarn'),
(8, 'homo', '****'),
(9, 'god-damn', 'goshdarn'),
(10, 'motherfucker', 'x'),
(11, 'fudgepacker', 'x'),
(12, 'slut', '****'),
(13, 'cunt', '****'),
(14, 'bullshit', 'bull****'),
(15, 'hoar', '****'),
(16, 'wanker', '******'),
(17, 'whore', '*****'),
(18, 'queer', '*****'),
(20, 'shit', '****'),
(21, 'asshole', '***hole'),
(22, 'asswipe', '***wipe'),
(23, 'biatches', '*'),
(24, 'putain', '******'),
(25, 'merde', '****'),
(30, 'puta', '****'),
(40, 'nigger', '******'),
(41, 'niga', '****'),
(42, 'nigga', '*****'),
(44, 'mutha fugga', '***** *****'),
(45, 'fuggin', 'poopy'),
(46, 'schlampe', '*'),
(47, 'fick', '*'),
(50, 'muschi', '*'),
(58, 'fagot', '*'),
(59, 'faggot', '*'),
(60, 'cock', '****'),
(61, 'jizz', '****'),
(62, 'tits', '****'),
(63, 'jode', '****'),
(64, 'stfu', 'sthu'),
(65, 'twat', '****'),
(66, 'gtfo', 'gtho'),
(68, 'mofo', '****'),
(69, 'ffs', '***'),
(70, 'wtf', 'wth'),
(71, 'omfg', 'omg'),
(72, 'pussies', '******'),
(73, 'niggas', '*****'),
(74, 'asswipes', '***wipes'),
(75, 'cocks', '*****'),
(76, 'faggots', '******'),
(77, 'queers', '*****'),
(78, 'shits', 'runs'),
(79, 'dicks', '*****'),
(80, 'bastards', '********'),
(81, 'cunts', '*****'),
(82, 'homos', '*****'),
(83, 'whores', '******'),
(84, 'wankers', '******'),
(85, 'fucking', '*******'),
(86, 'fucker', '******'),
(87, 'fuk', '***'),
(88, 'fuking', '******'),
(89, 'fuker', '*****'),
(90, 'hoars', '*****'),
(91, 'sluts', '*****'),
(92, 'penis', '*****'),
(93, 'vagina', '******');
