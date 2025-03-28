
-- --------------------------------------------------------

--
-- Table structure for table `questplayers`
--

CREATE TABLE `questplayers` (
  `QuestPlayersID` int(10) UNSIGNED NOT NULL,
  `QuestPlayerName` varchar(45) NOT NULL,
  `QuestID` varchar(45) NOT NULL,
  `QuestTimeStamp` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- Dumping data for table `questplayers`
--

INSERT INTO `questplayers` (`QuestPlayersID`, `QuestPlayerName`, `QuestID`, `QuestTimeStamp`) VALUES
(1, 'atomicstorm', '852', '2008-07-13 22:44:01'),
(2, 'Pakelika', '1534', '2008-07-14 02:11:43'),
(3, 'Pakelika', '852', '2008-07-14 02:11:57'),
(4, 'PaKeLiKa', '161', '2008-07-14 17:39:32'),
(5, 'PaKeLiKa', '1405', '2008-07-14 17:42:27'),
(6, 'turk8813', '1534', '2008-07-15 01:36:12'),
(7, 'Hobbit', '852', '2008-07-15 18:05:55'),
(8, 'Aaxel', '1453', '2008-07-15 18:45:27'),
(9, 'Aaxel', '1439', '2008-07-16 15:45:24'),
(10, 'Lghikas', '1534', '2008-07-18 03:54:46'),
(11, 'sheershock', '852', '2008-07-18 04:25:15'),
(12, 'sheershock', '1489', '2008-07-18 04:36:13'),
(13, 'sheershock', '1405', '2008-07-18 06:25:09'),
(14, 'sheershock', '1498', '2008-07-18 06:40:37'),
(15, 'sheershock', '1534', '2008-07-18 06:47:30'),
(16, 'sheershock', '1146', '2008-07-18 07:04:14'),
(17, 'sheershock', '1135', '2008-07-18 07:40:11'),
(18, 'sheershock', '368', '2008-07-18 07:56:49'),
(19, 'TheBuFFster', '1498', '2008-07-18 08:22:46'),
(20, 'GothicKratos', '1498', '2008-07-18 08:23:38'),
(21, 'GothicKratos', '852', '2008-07-18 09:28:21'),
(22, 'sheershock', '1534', '2008-07-21 03:04:50'),
(23, 'sheershock', '1534', '2008-07-21 03:05:46'),
(24, 'sheershock', '1534', '2008-07-21 03:06:38'),
(25, 'sheershock', '1534', '2008-07-21 03:07:21'),
(26, 'sheershock', '1534', '2008-07-21 03:08:19'),
(27, 'sheershock', '1534', '2008-07-21 03:28:20'),
(28, 'sheershock', '1534', '2008-07-21 03:28:46'),
(29, 'sheershock', '1534', '2008-07-21 03:30:13'),
(30, 'sheershock', '1534', '2008-07-21 03:32:04'),
(31, 'sheershock', '1534', '2008-07-21 03:33:24'),
(32, 'sheershock', '1534', '2008-07-21 03:44:19'),
(33, 'sheershock', '1534', '2008-07-21 03:44:38'),
(34, 'sheershock', '1534', '2008-07-21 03:45:45'),
(35, 'sheershock', '1534', '2008-07-21 03:46:03'),
(36, 'sheershock', '1534', '2008-07-21 03:46:24'),
(37, 'sheershock', '1534', '2008-07-21 04:48:23'),
(38, 'sheershock', '1534', '2008-07-21 06:46:49'),
(39, 'sheershock', '1534', '2008-07-21 06:47:28'),
(40, 'sheershock', '1534', '2008-07-21 06:48:04'),
(41, 'sheershock', '1534', '2008-07-21 06:50:02'),
(42, 'sheershock', '1534', '2008-07-21 06:52:01'),
(43, 'sheershock', '1534', '2008-07-21 06:52:35'),
(44, 'sheershock', '1534', '2008-07-21 06:53:24'),
(45, 'snookattack', '852', '2008-07-21 08:03:51'),
(46, 'sheershock', '1534', '2008-07-21 08:51:22'),
(47, 'sheershock', '1534', '2008-07-21 08:52:01'),
(48, 'sheershock', '1534', '2008-07-21 08:52:22'),
(49, 'sheershock', '1534', '2008-07-21 08:54:05'),
(50, 'sheershock', '1534', '2008-07-21 08:54:32'),
(51, 'sheershock', '1534', '2008-07-21 08:55:45'),
(52, 'sheershock', '1534', '2008-07-21 08:56:41'),
(53, 'sheershock', '1534', '2008-07-21 08:57:42'),
(54, 'sheershock', '1534', '2008-07-21 09:05:35'),
(55, 'sheershock', '1534', '2008-07-21 09:06:36'),
(56, 'sheershock', '1534', '2008-07-21 09:09:50'),
(57, 'sheershock', '1534', '2008-07-21 09:17:40'),
(58, 'sheershock', '1534', '2008-07-21 09:20:08'),
(59, 'sheershock', '1534', '2008-07-21 09:21:00'),
(60, 'sheershock', '1534', '2008-07-21 09:21:50'),
(61, 'ZeroInfinity', '1498', '2008-07-21 09:22:20'),
(62, 'sheershock', '1534', '2008-07-21 09:24:09'),
(63, 'sheershock', '1534', '2008-07-21 09:28:20'),
(64, 'sheershock', '1534', '2008-07-21 09:28:49'),
(65, 'sheershock', '1534', '2008-07-21 09:36:01'),
(66, 'ZeroInfinity', '161', '2008-07-21 09:45:39'),
(67, 'ZeroInfinity', '368', '2008-07-21 09:51:39'),
(68, 'sheershock', '1534', '2008-07-21 09:59:59'),
(69, 'sheershock', '1534', '2008-07-21 10:11:13'),
(70, 'sheershock', '1534', '2008-07-21 10:17:30'),
(71, 'sheershock', '1534', '2008-07-21 10:19:59'),
(72, 'sheershock', '1534', '2008-07-21 21:17:09'),
(73, 'sheershock', '1534', '2008-07-21 21:43:04'),
(74, 'sheershock', '1534', '2008-07-21 21:43:37'),
(75, 'sheershock', '1534', '2008-07-21 21:44:07'),
(76, 'sheershock', '1534', '2008-07-21 21:45:10'),
(77, 'sheershock', '1534', '2008-07-21 21:46:14'),
(78, 'sheershock', '1534', '2008-07-21 21:47:56'),
(79, 'sheershock', '1534', '2008-07-21 21:51:46'),
(80, 'sheershock', '1534', '2008-07-21 22:03:29'),
(81, 'sheershock', '1534', '2008-07-21 22:04:13'),
(82, 'sheershock', '1534', '2008-07-21 22:20:09'),
(83, 'sheershock', '1534', '2008-07-21 22:23:23'),
(84, 'sheershock', '1534', '2008-07-21 22:27:00'),
(85, 'sheershock', '1534', '2008-07-21 22:29:04'),
(86, 'PaKeLiKa', '1489', '2008-07-21 22:40:12'),
(87, 'sheershock', '1534', '2008-07-21 23:03:35'),
(88, 'sheershock', '1534', '2008-07-21 23:05:21'),
(89, 'sheershock', '1534', '2008-07-21 23:07:32'),
(90, 'sheershock', '1534', '2008-07-21 23:09:38'),
(91, 'sheershock', '1534', '2008-07-21 23:12:53'),
(92, 'sheershock', '1534', '2008-07-21 23:12:59'),
(93, 'sheershock', '1534', '2008-07-21 23:18:53'),
(94, 'sheershock', '1534', '2008-07-21 23:19:50'),
(95, 'sheershock', '1534', '2008-07-21 23:21:18'),
(96, 'sheershock', '1534', '2008-07-21 23:28:02'),
(97, 'sheershock', '1534', '2008-07-21 23:35:52'),
(98, 'sheershock', '1534', '2008-07-21 23:42:39'),
(99, 'sheershock', '1534', '2008-07-22 00:56:22'),
(100, 'Orideth', '1405', '2008-07-22 04:33:44'),
(101, 'PaKeLiKa', '1146', '2008-07-22 07:27:19'),
(102, 'Shadowblight', '1498', '2008-07-25 04:12:16'),
(103, 'Bugzapper', '852', '2008-07-25 21:06:50'),
(104, 'gringo355', '368', '2008-07-26 02:07:45'),
(105, 'gringo355', '1405', '2008-07-26 03:05:31'),
(106, 'Iceman17', '368', '2008-07-26 03:07:53'),
(107, 'DarkHeart', '1498', '2008-07-27 07:51:04'),
(108, 'GothicKratos', '1534', '2008-07-27 10:16:27'),
(109, 'GothicKratos', '1489', '2008-07-27 10:23:47'),
(110, 'GothicKratos', '1135', '2008-07-27 10:46:30'),
(111, 'GothicKratos', '1146', '2008-07-27 11:03:52'),
(112, 'DarkHeart', '1534', '2008-07-27 11:10:08'),
(113, 'GothicKratos', '1405', '2008-07-27 11:10:52'),
(114, 'DarkHeart', '1119', '2008-07-27 11:24:26'),
(115, 'DarkHeart', '1135', '2008-07-27 11:42:28'),
(116, 'DarkHeart', '1405', '2008-07-27 12:03:07'),
(117, 'DarkHeart', '1489', '2008-07-27 12:08:15'),
(118, 'DarkHeart', '1146', '2008-07-27 12:13:56'),
(119, 'DarkHeart', '852', '2008-07-27 12:59:06'),
(120, 'Drakza', '161', '2008-07-28 00:14:28'),
(121, 'Orideth', '148', '2008-07-28 08:13:08'),
(122, 'Orideth', '447', '2008-07-28 08:19:28'),
(123, 'Shadowblight', '1534', '2008-07-30 02:12:24'),
(124, 'SMaster777', '1534', '2008-07-31 10:49:09'),
(125, 'SMaster777', '447', '2008-07-31 10:53:40'),
(126, 'SMaster777', '161', '2008-08-01 04:28:19'),
(127, 'Neji1985', '852', '2008-08-01 18:28:32'),
(128, 'Bruin', '852', '2008-08-04 09:24:12'),
(129, 'brooks08', '1453', '2008-08-04 12:07:39'),
(130, 'brooks08', '1439', '2008-08-04 12:27:55'),
(131, 'brooks08', '1533', '2008-08-04 12:43:36'),
(132, 'MaidFetish', '1534', '2008-08-05 00:35:57'),
(133, 'DeviousPie', '1534', '2008-08-05 00:43:43'),
(134, 'MaidFetish', '447', '2008-08-05 00:43:54'),
(135, 'MaidFetish', '1405', '2008-08-05 00:53:42'),
(136, 'MaidFetish', '1498', '2008-08-05 01:01:52'),
(137, 'MaidFetish', '148', '2008-08-05 01:08:05'),
(138, 'DeviousPie', '1498', '2008-08-05 01:13:45'),
(139, 'DeviousPie', '1135', '2008-08-05 01:44:05'),
(140, 'MaidFetish', '368', '2008-08-05 01:48:45'),
(141, 'DeviousPie', '368', '2008-08-05 18:29:46'),
(142, 'MaidFetish', '852', '2008-08-05 20:46:08'),
(143, 'MaidFetish', '1489', '2008-08-05 20:47:07'),
(144, 'MaidFetish', '1146', '2008-08-05 20:52:20'),
(145, 'MaidFetish', '161', '2008-08-05 20:56:38'),
(146, 'MaidFetish', '26', '2008-08-05 21:47:34'),
(147, 'erael', '1534', '2008-08-05 23:02:19'),
(148, 'erael', '1498', '2008-08-05 23:08:40'),
(149, 'Burningats', '1453', '2008-08-06 17:44:02'),
(150, 'Burningats', '1533', '2008-08-06 17:51:08'),
(151, 'deadwalk2000', '1453', '2008-08-06 21:22:42'),
(152, 'deadwalk2000', '1533', '2008-08-08 10:45:24'),
(153, 'paulpridham7', '1533', '2008-08-09 15:15:00'),
(154, 'paulpridham7', '1453', '2008-08-09 15:24:00'),
(155, 'Darksol', '852', '2008-08-11 23:37:26'),
(156, 'Darksol', '447', '2008-08-11 23:56:14'),
(157, 'Darksol', '1405', '2008-08-12 00:00:00'),
(158, 'Darksol', '1489', '2008-08-12 00:02:20'),
(159, 'Darksol', '1534', '2008-08-12 00:04:03'),
(160, 'donnie3206', '852', '2008-08-13 00:58:01'),
(161, 'donnie3206', '1498', '2008-08-14 10:56:52'),
(162, 'donnie3206', '148', '2008-08-14 12:35:42'),
(163, 'pinkspider', '852', '2008-08-14 17:26:40'),
(164, 'pinkspider', '1405', '2008-08-15 15:13:45'),
(165, 'pinkspider', '1090', '2008-08-15 15:18:34'),
(166, 'pinkspider', '1534', '2008-08-15 18:06:37'),
(167, 'ZeroInfinity', '26', '2008-08-23 00:58:00'),
(168, 'GothicKratos', '368', '2008-08-28 00:47:51'),
(169, '251811918', '852', '2008-09-01 16:28:32'),
(170, 'Solitaire', '1405', '2008-09-04 19:11:00'),
(171, 'SquallEtrnly', '1533', '2008-09-09 02:38:31'),
(172, 'SquallEtrnly', '1439', '2008-09-09 02:51:48'),
(173, 'tsengraiden', '852', '2008-09-10 03:37:14'),
(174, 'onedestinazn', '852', '2008-09-11 19:05:15'),
(175, 'onedestinazn', '1146', '2008-09-11 19:29:43'),
(176, 'onedestinazn', '368', '2008-09-11 19:52:40'),
(177, 'crimsonking', '368', '2008-09-11 20:30:27'),
(178, 'penelo', '1533', '2008-09-19 21:11:53'),
(179, 'squll-typo', '1533', '2008-09-20 20:10:37'),
(180, 'darkersigns', '1533', '2008-10-07 10:38:38'),
(181, 'Lee768', '1533', '2008-10-31 20:43:13'),
(182, '', '1439', '2008-11-14 03:50:28'),
(183, 'y2trips', '852', '2009-01-09 03:46:31'),
(184, 'y2trips', '1534', '2009-01-09 03:58:24'),
(185, 'y2trips', '1489', '2009-01-09 04:00:06'),
(186, 'y2trips', '1146', '2009-01-09 04:01:24'),
(187, 'y2trips', '1090', '2009-01-09 05:21:57'),
(188, 'XiahouCougar', '852', '2009-01-10 22:49:20'),
(189, 'BlazinZix', '1405', '2009-01-11 03:43:43'),
(190, 'y2trips', '1762', '2009-01-14 01:56:20'),
(191, 'y2trips', '551', '2009-01-14 01:59:08'),
(192, 'y2trips', '1723', '2009-01-14 02:00:24'),
(193, 'y2trips', '881', '2009-01-14 02:59:01'),
(194, 'y2trips', '1339', '2009-01-14 03:00:17'),
(195, 'y2trips', '1743', '2009-01-14 03:13:04'),
(196, 'y2trips', '1626', '2009-01-14 03:18:48'),
(197, 'y2trips', '1144', '2009-01-14 04:38:48'),
(198, 'y2trips', '1028', '2009-01-14 04:50:21'),
(199, 'y2trips', '1835', '2009-01-14 05:21:09'),
(200, 'shooterjack', '852', '2009-01-14 06:04:21'),
(201, 'shooterjack', '1146', '2009-01-14 06:10:48'),
(202, 'shooterjack', '1626', '2009-01-14 06:29:26'),
(203, 'y2trips', '1214', '2009-01-15 00:12:53'),
(204, 'y2trips', '1031', '2009-01-15 00:23:20'),
(205, 'mickey5252', '852', '2009-01-16 02:01:20'),
(206, 'PaKeLiKa', '551', '2009-01-16 21:16:45'),
(207, 'PaKeLiKa', '1743', '2009-01-16 21:24:37'),
(208, 'fatcakes', '1144', '2009-01-17 06:28:02'),
(209, 'onedestinazn', '551', '2009-01-17 09:56:28'),
(210, 'Sunstar259', '551', '2009-01-17 09:57:50'),
(211, 'mickey5252', '1656', '2009-01-17 10:00:44'),
(212, 'onedestinazn', '1626', '2009-01-17 10:07:26'),
(213, 'onedestinazn', '1339', '2009-01-17 10:11:01'),
(214, 'onedestinazn', '1031', '2009-01-17 10:19:37'),
(215, 'Sunstar259', '1626', '2009-01-17 10:20:12'),
(216, 'onedestinazn', '923', '2009-01-17 10:21:18'),
(217, 'PaKeLiKa', '923', '2009-01-17 10:24:37'),
(218, 'mickey5252', '1676', '2009-01-17 10:28:02'),
(219, 'PaKeLiKa', '1144', '2009-01-17 10:34:26'),
(220, 'PaKeLiKa', '881', '2009-01-17 10:43:08'),
(221, 'onedestinazn', '1028', '2009-01-17 10:49:06'),
(222, 'onedestinazn', '45', '2009-01-17 11:00:23'),
(223, 'onedestinazn', '1676', '2009-01-18 05:49:37'),
(224, 'PaKeLiKa', '1028', '2009-01-19 13:36:46'),
(225, 'PaKeLiKa', '1835', '2009-01-19 13:37:35'),
(226, 'PaKeLiKa', '1656', '2009-01-19 23:39:55'),
(227, 'Dave420', '1836', '2009-01-20 03:23:53'),
(228, 'ladydeath', '852', '2009-01-20 09:56:07'),
(229, 'ladydeath', '1656', '2009-01-20 10:40:31'),
(230, 'ladydeath', '1723', '2009-01-20 11:04:18'),
(231, 'megan123', '1533', '2009-01-21 15:33:31'),
(232, 'sheershock', '881', '2009-01-25 16:21:08'),
(233, 'sheershock', '1626', '2009-01-25 16:23:10'),
(234, 'SpunkNik', '1533', '2009-01-28 11:31:19'),
(235, 'SpunkNik', '1683', '2009-01-28 12:02:33'),
(236, 'crimsonking', '1144', '2009-01-29 07:54:49'),
(237, 'crimsonking', '852', '2009-01-29 10:19:52'),
(238, 'crimsonking', '1656', '2009-01-29 10:29:36'),
(239, 'Bugzapper', '1534', '2009-02-01 19:51:58'),
(240, 'himself', '852', '2009-02-02 03:06:49'),
(241, 'himself', '1656', '2009-02-02 03:21:45'),
(242, 'himself', '1146', '2009-02-02 03:28:42'),
(243, 'mickey5252', '1534', '2009-02-04 00:44:20'),
(244, 'himself', '1028', '2009-02-04 00:55:58'),
(245, 'Zuggers', '852', '2009-02-05 10:56:06'),
(246, 'pinkspider', '1656', '2009-02-05 19:14:43'),
(247, 'pinkspider', '1489', '2009-02-05 19:14:58'),
(248, 'pinkspider', '1146', '2009-02-05 21:24:13'),
(249, 'mickey5252', '881', '2009-02-07 13:43:16'),
(250, 'redfire', '1656', '2009-02-07 23:42:59'),
(251, 'himself', '1135', '2009-02-07 08:00:00'),
(252, 'himself', '1835', '2009-02-08 21:27:34'),
(253, 'Zeromus', '852', '2009-02-09 22:41:24'),
(254, 'Zeromus', '1656', '2009-02-09 22:52:07'),
(255, 'Zeromus', '1339', '2009-02-10 02:29:14'),
(256, 'Cattrin', '852', '2009-02-10 18:38:00'),
(257, 'Cattrin', '1680', '2009-02-10 18:48:17'),
(258, 'Cattrin', '1762', '2009-02-10 18:49:49'),
(259, 'sheershock', '45', '2009-02-11 08:30:15'),
(260, 'sheershock', '1028', '2009-02-11 08:40:17'),
(261, 'sheershock', '1144', '2009-02-11 08:46:10'),
(262, 'sheershock', '1762', '2009-02-11 09:00:23'),
(263, 'TheBuFFster', '1146', '2009-02-13 07:07:25'),
(264, 'TheBuFFster', '852', '2009-02-13 07:10:57'),
(265, 'TheBuFFster', '1534', '2009-02-13 07:12:51'),
(266, 'himself', '923', '2009-02-16 22:38:51'),
(267, 'Grystor', '852', '2009-02-17 09:34:15'),
(268, 'Grystor', '1339', '2009-02-17 09:43:20'),
(269, 'sheershock', '1743', '2009-02-17 11:25:02'),
(270, 'pinkspider', '1762', '2009-02-26 15:13:15'),
(271, 'sissyhawk814', '1534', '2009-02-27 14:09:01'),
(272, 'sissyhawk814', '1146', '2009-02-27 14:13:50'),
(273, 'sissyhawk814', '852', '2009-02-27 14:18:20'),
(274, 'sissyhawk814', '923', '2009-02-27 14:23:26'),
(275, 'atomicstorm', '1028', '2009-02-27 22:11:55'),
(276, 'Valhalla', '852', '2009-02-28 07:28:38'),
(277, 'Valhalla', '1656', '2009-02-28 07:52:01'),
(278, 'Jinvicious', '368', '2009-03-04 08:34:41'),
(279, 'himself', '1743', '2009-03-06 07:04:51'),
(280, 'Valhalla', '1680', '2009-03-07 04:34:32'),
(281, 'Valhalla', '1662', '2009-03-07 04:50:32'),
(282, 'himself', '368', '2009-03-08 22:57:52'),
(283, 'atomicstorm', 'ID17806', '2009-03-14 03:52:43'),
(284, 'squll-typo', 'ID17806', '2009-03-14 03:53:09'),
(285, 'himself', 'ID22635', '2009-03-14 03:53:57'),
(286, 'atomicstorm', 'ID6124', '2009-03-17 19:18:13'),
(287, 'squll-typo', 'ID5942', '2009-03-17 19:20:32'),
(288, 'kamilbio112', 'ID4408', '2009-03-17 19:21:52'),
(289, 'ffantasy8', 'ID4408', '2009-03-17 19:22:04'),
(290, 'Crystigue', 'ID6124', '2009-03-17 19:58:17'),
(291, 'crimsonking', '1676', '2009-03-19 07:06:59'),
(292, 'y2trips', 'ID6153', '2009-03-21 21:33:59'),
(293, 'crimsonking', 'ID5186', '2009-03-23 19:42:49'),
(294, 'TSunami', 'ID6153', '2009-03-26 17:08:41'),
(295, '', 'ID5977', '2009-04-01 18:39:52'),
(296, 'darkersigns', 'ID4408', '2009-04-03 04:27:53'),
(297, 'cryptic', 'ID6153', '2009-04-06 05:35:01'),
(298, 'Andrew', 'ID6153', '2009-04-06 11:29:04'),
(299, 'Grimmy', 'ID3931', '2009-04-06 15:08:46'),
(300, 'bazandsue', 'ID4408', '2009-04-08 09:05:55'),
(301, 'LMaruko', 'ID6153', '2009-04-18 08:23:46'),
(302, 'retler', '852', '2009-04-20 16:26:25'),
(303, 'zambakblaze', 'ID5818', '2009-04-22 17:08:24');
