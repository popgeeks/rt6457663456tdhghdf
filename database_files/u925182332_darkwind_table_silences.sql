
-- --------------------------------------------------------

--
-- Table structure for table `silences`
--

CREATE TABLE `silences` (
  `id` int(10) UNSIGNED NOT NULL,
  `player` varchar(45) NOT NULL,
  `minutes` int(10) UNSIGNED NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci ROW_FORMAT=COMPACT;

--
-- Dumping data for table `silences`
--

INSERT INTO `silences` (`id`, `player`, `minutes`) VALUES
(106, 'darkavenger2', 5),
(112, 'AzureOrca', 4),
(173, 'PieDoom', 5),
(174, 'ianzar', 4),
(180, 'iloveyoutoo', 4),
(181, 'notmehonest', 5),
(182, 'hahaipban', 5),
(183, 'hahaipban', 5),
(184, 'wherearethey', 4),
(211, 'Finger', 4),
(228, 'ashe', 4),
(230, 'SandRock', 5),
(280, 'srt', 5),
(488, 'akiracode', 4),
(489, 'AkiraCode', 5),
(490, 'AkiraCode', 5),
(502, 'PuffDwagon', 4),
(508, 'brucehollett', 3),
(516, 'pennywise', 5),
(536, 'pennywise', 5),
(546, 'pennywise', 5),
(547, 'mirage-bot', 5),
(548, 'ashley-bot', 5),
(550, 'crab-bot', 5),
(561, 'admin-bot', 5),
(563, 'patriodsx', 5),
(564, 'fucubitch', 5);
