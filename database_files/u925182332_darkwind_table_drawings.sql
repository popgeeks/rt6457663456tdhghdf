
-- --------------------------------------------------------

--
-- Table structure for table `drawings`
--

CREATE TABLE `drawings` (
  `id` int(10) UNSIGNED NOT NULL,
  `prize1` varchar(45) NOT NULL DEFAULT '',
  `prize2` varchar(45) NOT NULL DEFAULT '',
  `prize3` varchar(45) NOT NULL DEFAULT '',
  `date` varchar(45) NOT NULL DEFAULT ''
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- Dumping data for table `drawings`
--

INSERT INTO `drawings` (`id`, `prize1`, `prize2`, `prize3`, `date`) VALUES
(1, 'darklumina', 'darklumina', 'darklumina', ''),
(2, 'darklumina', 'darklumina', 'darklumina', '08-23-2005'),
(3, 'darklumina', 'darklumina', 'darklumina', '08-23-2005'),
(4, 'Gilgamesh', 'rinoapereira', 'TheOneJesus7', '08-27-2005'),
(5, 'meh2', 'ZypherX', 'ukimaro', '09-03-2005'),
(6, 'evilweeman', 'ColdArmy', 'supersyd', '09-10-2005'),
(7, 'Raya', 'Morgen', 'debussy', '09-17-2005'),
(8, 'rxbrady', 'LockieLaets', 'TheOneJesus7', '09-24-2005'),
(9, 'meh2', 'Tears', 'LisaJ', '10-01-2005'),
(10, 'cent5', 'Sephirothbmx', 'HipHopHater', '10-08-2005'),
(11, 'crazyelf48', 'crazyelf48', 'crazyelf48', '10-15-2005'),
(12, 'guitardude86', 'dark-wing', 'BlackAngel', '10-22-2005'),
(13, 'Diabolo', 'BlackAngel', 'LagunaD', '10-29-2005'),
(14, 'SHIVA', 'crazyelf48', 'Quicksilver', '11-05-2005'),
(15, 'shadowfoxuk', 'ismhawk', 'howabe', '11-12-2005'),
(16, 'BlackAngel', 'ColdArmy', 'darkelfgil', '11-19-2005'),
(17, 'donnie3206', 'Trebek', 'darklumina', '11-26-2005'),
(18, 'Adinsx', 'labmonkey', 'BlackAngel', '12-03-2005'),
(19, 'Omni', 'Omni', 'PhatRabbit', '12-10-2005'),
(20, 'ukimaro', 'PaKeLiKa', 'skirtboy', '12-17-2005'),
(21, 'redevilchild', 'DarkHaven', 'amy', '12-24-2005'),
(22, 'PhatRabbit', 'tranvinh', 'DarkNarga', '12-31-2005'),
(23, 'rusty55cheuy', 'ukimaro', 'ukimaro', '01-07-2006'),
(24, 'Omni', 'x1t4cHix', 'ColdArmy', '01-14-2006');
