
-- --------------------------------------------------------

--
-- Table structure for table `playerlootcards`
--

CREATE TABLE `playerlootcards` (
  `ID` int(10) UNSIGNED NOT NULL,
  `LootCardID` int(10) UNSIGNED NOT NULL,
  `Player` varchar(45) NOT NULL,
  `Value` int(10) UNSIGNED NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci ROW_FORMAT=COMPACT;

--
-- Dumping data for table `playerlootcards`
--

INSERT INTO `playerlootcards` (`ID`, `LootCardID`, `Player`, `Value`) VALUES
(1, 22, 'atomicstorm', 54),
(4, 22, 'mdemaz2', 0),
(5, 22, 'mysticnickel', 0),
(10, 23, 'onedestinazn', 0),
(12, 4, 'ZeroInfinity', 0),
(13, 4, 'atomicstorm', 1),
(14, 23, 'y2trips', 1),
(17, 5, 'AsKa', 0),
(18, 4, 'mdemaz2', 1),
(19, 11, 'onedestinazn', 0),
(20, 4, 'mysticnickel', 0),
(21, 12, 'mysticnickel', 0),
(24, 12, 'mitsu', 0),
(26, 23, 'mdemaz2', 0),
(28, 12, 'onedestinazn', 0),
(29, 15, 'ZeroInfinity', 0),
(30, 11, 'Darksol', 0),
(34, 4, 'mitsu', 0),
(35, 11, 'Pseudocake', 0),
(37, 4, 'Jinvicious', 0),
(39, 13, 'mitsu', 0),
(40, 22, 'Kuisami', 0),
(42, 16, 'PaKeLiKa', 0),
(43, 22, 'PaKeLiKa', 0),
(45, 16, 'Athelston', 0),
(46, 15, 'SpunkNik', 0),
(49, 16, 'Pseudocake', 0),
(51, 4, 'PaKeLiKa', 0),
(52, 23, 'PaKeLiKa', 0),
(53, 4, 'Totti10', 0),
(54, 12, 'Totti10', 0),
(55, 15, 'onedestinazn', 0),
(57, 23, 'Phoenix243', 0),
(59, 22, 'squll-typo', 1),
(61, 11, 'ShadowM', 0),
(62, 2, 'PaKeLiKa', 0),
(67, 23, 'Darksol', 0),
(68, 22, 'Darksol', 0),
(69, 11, 'PaKeLiKa', 0),
(70, 13, 'PaKeLiKa', 0),
(72, 12, 'ForeverZ3RO', 0),
(73, 23, 'darkersigns', 0),
(77, 13, 'TheBuFFster', 0),
(79, 4, 'monmas', 4),
(80, 23, 'Jinvicious', 2),
(84, 16, 'Bugzapper', 1),
(85, 3, 'Ballie19', 0),
(86, 22, 'Ballie19', 1),
(87, 22, 'SpunkNik', 1),
(88, 4, 'ForeverZ3RO', 0),
(89, 22, 'Totti10', 0),
(90, 22, 'bazandsue', 0),
(91, 12, 'Grimmy', 0),
(92, 2, 'ForeverZ3RO', 0),
(93, 2, 'Hotaro', 0),
(95, 11, 'y2trips', 0),
(96, 22, 'TheBuFFster', 0),
(98, 16, 'mdemaz2', 0),
(102, 3, 'VashAngel', 0),
(103, 3, 'Ryoshin', 0),
(109, 16, 'amy2204', 1),
(114, 4, 'ShadowM', 0),
(116, 12, 'Sunstar259', 2),
(117, 12, 'bazandsue', 0),
(119, 12, 'aska', 0),
(120, 13, 'Slithered', 0),
(121, 15, 'jackie', 0),
(122, 13, 'Dioza', 0),
(123, 16, 'Dioza', 0),
(124, 23, 'ForeverZ3RO', 1),
(126, 22, 'vale', 0),
(128, 23, 'bournelegend', 0),
(129, 22, 'Derfas13', 1),
(130, 11, 'SpunkNik', 0),
(131, 5, 'SpunkNik', 14),
(132, 12, 'sexyshinobu', 0),
(133, 5, 'sexyshinobu', 0),
(137, 4, 'Hotaro', 0),
(138, 16, 'Hotaro', 0),
(139, 15, 'Squeakster21', 0),
(140, 16, 'Mugi', 1),
(142, 5, 'VashAngel', 1),
(144, 4, 'bazandsue', 0),
(145, 13, 'bazandsue', 0),
(146, 5, 'shooterjack', 0),
(148, 16, 'Blitzer', 0),
(152, 5, 'ShadowM', 0),
(153, 12, 'ShadowM', 1),
(156, 5, 'atomicstorm', 0),
(157, 5, 'SMaster777', 3),
(160, 5, 'y2trips', 1),
(161, 16, 'y2trips', 0),
(162, 16, 'Glacial89', 0),
(163, 4, 'Dioza', 0),
(164, 4, 'Slithered', 1),
(166, 5, 'eliteburner', 0),
(168, 4, 'VashAngel', 0),
(169, 4, 'Ryoshin', 0),
(170, 11, 'Ryoshin', 0),
(171, 11, 'Glacial89', 0),
(172, 22, 'Glacial89', 0),
(173, 5, 'squll-typo', 0),
(174, 12, 'squll-typo', 0),
(175, 5, 'PaKeLiKa', 0),
(176, 5, 'amy2204', 1),
(178, 16, 'monmas', 0),
(180, 12, 'Crown', 3),
(181, 5, 'Crown', 0),
(183, 11, 'Solidsnake', 1),
(184, 1, 'eliteburner', 0),
(186, 5, 'Sunstar259', 0),
(188, 23, 'shooterjack', 0),
(190, 2, 'monmas', 0),
(191, 2, 'Jinvicious', 0),
(193, 15, 'monmas', 0),
(194, 15, 'Jinvicious', 0),
(196, 23, 'bazandsue', 0),
(197, 11, 'mdemaz2', 2),
(198, 12, 'SpunkNik', 0),
(200, 5, 'Pseudocake', 0),
(201, 12, 'Pseudocake', 1),
(202, 5, 'bournelegend', 0),
(204, 5, 'wikaitaia', 0),
(205, 12, 'wikaitaia', 0),
(206, 22, 'wikaitaia', 0),
(207, 11, 'wikaitaia', 0),
(209, 5, 'Derfas13', 5),
(212, 5, 'nazza', 1),
(216, 16, 'wikaitaia', 0),
(218, 16, 'eliteburner', 0),
(220, 23, 'Slithered', 0),
(221, 13, 'Power', 0),
(222, 23, 'monmas', 1),
(223, 12, 'Drakza', 1),
(224, 5, 'Drakza', 2),
(227, 11, 'Crown', 1),
(229, 14, 'power', 0),
(230, 23, 'Glacial89', 0),
(231, 12, 'Glacial89', 0),
(238, 16, 'bazandsue', 0),
(239, 5, 'ZeroInfinity', 0),
(240, 22, 'ZeroInfinity', 0),
(241, 4, 'AsKa', 0),
(245, 14, 'Glacial89', 0),
(246, 11, 'mitsu', 0),
(248, 15, 'Glacial89', 0),
(249, 4, 'Jonty', 0),
(250, 22, 'Jonty', 0),
(251, 14, 'monmas', 0),
(252, 14, 'JinVicious', 0),
(253, 14, 'bazandsue', 0),
(255, 5, 'ponyboyxiii', 0),
(256, 12, 'ponyboyxiii', 0),
(257, 4, 'Gringo335', 0),
(258, 12, 'Gringo335', 0),
(259, 4, 'Zeromus', 0),
(260, 12, 'Zeromus', 0),
(267, 5, 'monmas', 0),
(268, 5, 'LostProphet', 0),
(270, 5, 'Totti10', 0),
(271, 5, 'Slithered', 0),
(273, 16, 'Darksol', 0),
(274, 22, 'AsKa', 0),
(275, 11, 'Sunstar259', 1),
(278, 23, 'Pseudocake', 0),
(279, 5, 'bazandsue', 0),
(280, 5, 'Glacial89', 0),
(284, 5, 'Bugzapper', 0),
(289, 22, 'Necromance', 1),
(290, 5, 'Necromance', 0),
(291, 23, 'Necromance', 2),
(293, 5, 'hentaininja', 0),
(294, 16, 'Jonty', 0),
(296, 5, 'Grimmy', 0),
(297, 23, 'Grimmy', 2),
(300, 5, 'J102y', 0),
(301, 23, 'J102y', 1),
(302, 5, 'Ayuna', 0),
(305, 12, 'Slithered', 0),
(307, 5, 'espershiva', 0),
(310, 22, 'Espershiva', 0),
(311, 22, 'Jinvicious', 1),
(312, 22, 'holzbeen', 1),
(313, 5, 'holzbeen', 1),
(315, 5, 'Darksol', 0),
(316, 15, 'Darksol', 0),
(318, 23, 'girzim69', 0),
(319, 5, 'Power', 0),
(322, 4, 'Glacial89', 0),
(324, 23, 'Sunstar259', 4),
(325, 5, 'SeeDRankA420', 1),
(326, 11, 'bazandsue', 1),
(331, 26, 'mdemaz2', 0),
(332, 29, 'ZeroInfinity', 0),
(334, 4, 'Darksol', 0),
(335, 4, 'LostProphet', 0),
(336, 15, 'zexion', 0),
(338, 26, 'Ryoshin', 0),
(340, 22, 'ekibiogami', 4),
(341, 29, 'ShadowM', 0),
(342, 29, 'Glacial89', 0),
(343, 29, 'Jinvicious', 0),
(345, 5, 'mizuryuu83', 0),
(346, 4, 'ekibiogami', 0),
(347, 5, 'darkjaxychan', 4),
(349, 4, 'Espershiva', 0),
(351, 15, 'darkjaxychan', 0),
(353, 23, 'ZackFairrr', 0),
(355, 26, 'Darksol', 0),
(356, 26, 'ekibiogami', 2),
(358, 22, 'repede', 0),
(359, 12, 'Darksol', 0),
(361, 5, 'ekibiogami', 16),
(363, 15, 'truezigfrid', 0),
(365, 22, 'PonyboyXIII', 0),
(367, 5, 'mitsu', 11),
(368, 5, 'Jinvicious', 0),
(369, 26, 'LostProphet', 0),
(370, 5, 'truezigfrid', 0),
(371, 16, 'battousai98', 0),
(372, 15, 'Kimhy', 0),
(373, 16, 'Derfas13', 0),
(374, 26, 'Crown', 1),
(375, 12, 'Ayuna', 0),
(377, 15, 'bazandsue', 0),
(378, 29, 'bazandsue', 0),
(380, 5, 'legendary', 4),
(381, 26, 'Sunstar259', 7),
(383, 25, 'Slithered', 0),
(385, 29, 'jackie', 0),
(386, 16, 'Espershiva', 0),
(388, 25, 'mdemaz2', 0),
(389, 15, 'ekibiogami', 0),
(390, 12, 'darkjaxychan', 0),
(391, 12, 'monmas', 0),
(392, 11, 'Ekibiogami', 0),
(393, 4, 'Derfas13', 0),
(394, 16, 'AsKa', 0),
(395, 12, 'mizuryuu83', 0),
(396, 25, 'wikaitaia', 0),
(397, 16, 'Team', 1),
(400, 4, 'SpunkNik', 2),
(401, 22, 'eliteburner', 1),
(402, 15, 'Power', 0),
(403, 11, 'Totti10', 0),
(404, 4, 'solidsnake', 0),
(408, 22, 'mizuryuu83', 0),
(409, 11, 'jackie', 0),
(410, 11, 'Team', 2),
(412, 16, 'JinVicious', 0),
(414, 5, 'Team', 0),
(415, 25, 'bazandsue', 0),
(416, 5, 'minigunner', 0),
(418, 5, 'mdemaz2', 1),
(419, 4, 'battousai98', 0),
(420, 25, 'monmas', 0),
(421, 4, 'truezigfrid', 0),
(422, 11, 'bournelegend', 0),
(427, 23, 'SMaster777', 2),
(428, 15, 'mizuryuu83', 0),
(429, 26, 'wikaitaia', 0),
(433, 5, 'battousai98', 0),
(434, 5, 'girzim69', 0),
(435, 25, 'Totti10', 0),
(436, 4, 'akatashi', 0),
(438, 16, 'Totti10', 0),
(441, 12, 'jackie', 1),
(442, 16, 'Sunstar259', 4),
(443, 5, 'export', 0),
(445, 29, 'battousai98', 0),
(448, 22, 'Pseudocake', 1),
(450, 4, 'minigunner', 0),
(454, 11, 'truezigfrid', 0),
(455, 16, 'Daekesh', 0),
(456, 5, 'Daekesh', 0),
(458, 26, 'Slithered', 0),
(459, 15, 'sequeira', 0),
(460, 11, 'Marques', 1),
(461, 26, 'SMaster777', 1),
(462, 23, 'Darkglory', 0),
(465, 23, 'legendary', 1),
(467, 11, 'legendary', 1),
(469, 23, 'Derfas13', 1),
(476, 25, 'Drakza', 1),
(477, 16, 'sofaloaf', 1),
(479, 25, 'SpunkNik', 2),
(482, 5, 'Fengsten', 0),
(483, 5, 'Darkglory', 0),
(484, 26, 'shooterjack', 0),
(485, 26, 'Lausier', 0),
(486, 5, 'Lausier', 0),
(487, 16, 'SephirothN76', 1),
(488, 23, 'kamilbio112', 1),
(489, 5, 'kamilbio112', 0),
(490, 5, 'Kuisami', 0),
(492, 23, 'wikaitaia', 0),
(494, 29, 'eXport', 0),
(495, 23, 'Fengsten', 0),
(496, 5, 'Noctis14', 0),
(501, 4, 'jackie', 0),
(503, 4, 'Lausier', 0),
(506, 14, 'mitsu', 0),
(507, 26, 'SpunkNik', 1),
(508, 23, 'ShadowM', 0),
(510, 16, 'murder152', 1),
(512, 16, 'mitsu', 0),
(519, 16, 'Stilzkin', 0),
(522, 26, 'ShadowM', 0),
(524, 16, 'ShadowM', 0),
(527, 22, 'Hellome0', 0),
(528, 12, 'eliteburner', 0),
(530, 16, 'Darkglory', 0),
(531, 22, 'onedestinazn', 0),
(541, 15, 'y2trips', 0),
(542, 15, 'Necromance', 0),
(543, 16, 'Aaxel', 0),
(544, 4, 'Hope', 0),
(545, 12, 'Hope', 1),
(546, 5, 'Hope', 0),
(547, 13, 'Fengsten', 0),
(548, 23, 'ekibiogami', 0),
(550, 15, 'Hope', 0),
(551, 23, 'Gringo335', 0),
(552, 22, 'jackie', 5),
(554, 16, 'Spoon', 0),
(555, 11, 'drognan1470', 1),
(556, 12, 'Fengsten', 0),
(557, 11, 'Fengsten', 0),
(558, 25, 'y2trips', 0),
(559, 4, 'Shuyin987', 0),
(560, 11, 'Shuyin987', 0),
(562, 5, 'Nall', 0),
(566, 13, 'J102y', 0),
(567, 13, 'Shuyin987', 0),
(568, 1, 'bazandsue', 0),
(569, 26, 'bazandsue', 1),
(570, 23, 'mizuryuu83', 0),
(571, 26, 'y2trips', 0),
(572, 5, 'Stilzkin', 8),
(574, 5, 'legendfox', 1),
(576, 5, 'xxkniferxx', 0),
(577, 22, 'Shuyin987', 0),
(579, 4, 'y2trips', 0),
(580, 1, 'monmas', 1),
(582, 13, 'Necromance', 0),
(583, 1, 'scsuperstar', 0),
(584, 4, 'onedestinazn', 1),
(585, 25, 'onedestinazn', 1),
(587, 16, 'jackie', 1),
(588, 11, 'Gringo335', 0),
(589, 2, 'Gringo335', 0),
(590, 26, 'jackie', 1),
(591, 2, 'Derfas13', 0),
(592, 3, 'Gringo335', 0),
(593, 3, 'ZackFairrr', 0),
(594, 4, 'export', 0),
(595, 29, 'Gringo335', 0),
(600, 26, 'Gringo335', 0),
(603, 1, 'Gringo335', 0),
(605, 25, 'xxkniferxx', 0),
(606, 16, 'Necromance', 0),
(607, 14, 'Necromance', 0),
(608, 22, 'Gringo335', 0),
(609, 11, 'Aaxel', 1),
(610, 5, 'Gringo335', 0),
(611, 5, 'Spoon', 0),
(612, 4, 'phoneshoe', 0),
(613, 4, 'ZackFairrr', 0),
(616, 23, 'jackie', 4),
(617, 5, 'truesorakh', 0),
(620, 26, 'mizuryuu83', 0),
(622, 12, 'y2trips', 0),
(623, 5, 'MeWantTaco', 0),
(624, 5, 'phoneshoe', 0),
(625, 23, 'phoneshoe', 0),
(626, 3, 'mizuryuu83', 0),
(627, 3, 'phoneshoe', 0),
(628, 25, 'phoneshoe', 1),
(629, 12, 'Leshrac99', 0),
(631, 25, 'Darksol', 0),
(633, 4, 'Necromance', 0),
(635, 22, 'az', 1),
(636, 25, 'battousai98', 0),
(639, 4, 'mizuryuu83', 0),
(640, 23, 'export', 1),
(641, 12, 'ekibiogami', 0),
(643, 16, 'MeWantTaco', 1),
(646, 5, 'corin3690', 0),
(648, 11, 'alukardian', 0),
(650, 5, 'alukardian', 0),
(652, 5, 'bamboo', 0),
(654, 4, 'alukardian', 0),
(655, 26, 'eXport', 0),
(656, 25, 'Derfas13', 1),
(658, 4, 'Fengsten', 0),
(660, 28, 'Fengsten', 0),
(661, 14, 'Fengsten', 0),
(662, 28, 'jackie', 0),
(665, 5, 'ZackFairrr', 0),
(667, 23, 'SpunkNik', 1),
(671, 11, 'lee3825', 0),
(673, 22, 'bamboo', 0),
(674, 11, 'mizuryuu83', 0),
(675, 14, 'Slithered', 0),
(677, 15, 'alukardian', 0),
(678, 13, 'Kuisami', 0),
(680, 23, 'jammy', 0),
(681, 5, 'Leshrac99', 0),
(682, 22, 'ShadowM', 1),
(683, 12, 'alukardian', 0),
(684, 29, 'mizuryuu83', 0),
(686, 22, 'y2trips', 1),
(687, 25, 'lee3825', 0),
(691, 4, 'amy', 0),
(692, 12, 'amy', 0),
(693, 5, 'Zexion', 0),
(695, 16, 'squll-typo', 1),
(696, 15, 'Gringo335', 0),
(699, 26, 'Leshrac99', 0),
(700, 16, 'Ekibiogami', 0),
(701, 14, 'Gringo335', 0),
(702, 28, 'bazandsue', 0),
(703, 12, 'SolarEclipse', 0),
(704, 14, 'SolarEclipse', 0),
(705, 14, 'nazza', 0),
(707, 26, 'lee3825', 0),
(709, 25, 'alukardian', 0),
(710, 15, 'SolarEclipse', 0),
(713, 14, 'TheBuFFster', 0),
(715, 14, 'Leshrac99', 0),
(717, 26, 'TheBuFFster', 0),
(718, 12, 'Necromance', 0),
(720, 5, 'jonty', 0),
(721, 12, 'starker', 0),
(722, 11, 'lloydred', 0),
(723, 11, 'masterraiden', 0),
(724, 23, 'TheBuFFster', 0),
(727, 5, 'caiman', 1),
(728, 5, 'adrammelech7', 0),
(730, 4, 'TheBuFFster', 0),
(731, 5, 'TheBuFFster', 0),
(732, 15, 'caiman', 0),
(733, 16, 'alukardian', 1),
(735, 26, 'Kimhy', 0),
(737, 28, 'TheBuFFster', 0),
(738, 27, 'TheBuFFster', 0),
(739, 22, 'ZackFairrr', 0),
(740, 16, 'cbowling', 0),
(741, 16, 'Power', 0),
(743, 11, 'Slithered', 0),
(745, 29, 'caiman', 0),
(746, 23, 'caiman', 1),
(748, 23, 'krigersk', 2),
(749, 25, 'caiman', 1),
(751, 25, 'TheBuFFster', 0),
(753, 29, 'Serax', 0),
(756, 5, 'Roxas20', 0),
(757, 23, 'PonyboyXIII', 3),
(758, 3, 'alukardian', 0),
(759, 3, 'Zeromus', 0),
(761, 3, 'Avarax', 0),
(762, 25, 'jackie', 2),
(763, 25, 'VashAngel', 0),
(764, 25, 'Boko', 0),
(765, 5, 'Dantushk', 0),
(768, 1, 'Darksol', 0),
(769, 1, 'Yukarie', 1),
(771, 4, 'crono423', 0),
(774, 4, 'Slayerfortyk', 0),
(776, 25, 'slayerfortyk', 1),
(778, 5, 'Yukarie', 10),
(780, 25, 'martbus', 0),
(781, 4, 'Avarax', 0),
(784, 26, 'Avarax', 0),
(787, 5, 'jackie', 0),
(789, 4, 'martbus', 0),
(790, 4, 'PonyboyXIII', 1),
(791, 5, 'Slayerfortyk', 0),
(796, 5, 'Zeromus', 0),
(799, 5, 'Jimmy3000', 0),
(800, 23, 'Boko', 0),
(801, 4, 'seedranka420', 0),
(802, 16, 'martbus', 0),
(803, 23, 'Roxas20', 3),
(805, 11, 'Avarax', 0),
(807, 5, 'Avarax', 0),
(810, 26, 'Phoenix243', 0),
(811, 16, 'Phoenix243', 0),
(812, 5, 'Phoenix243', 0),
(815, 11, 'Phoenix243', 0),
(816, 12, 'Phoenix243', 0),
(817, 11, 'martbus', 0),
(818, 12, 'martbus', 0),
(819, 26, 'Zeromus', 3),
(821, 23, 'Avarax', 4),
(822, 16, 'Avarax', 0),
(823, 16, 'GrieverPlus', 0),
(824, 23, 'JesseDoetsch', 0),
(825, 16, 'Zeromus', 0),
(826, 26, 'alukardian', 0),
(827, 16, 'SpunkNik', 0),
(830, 16, 'knightmare11', 0),
(831, 14, 'knightmare11', 0),
(832, 5, 'JesseDoetsch', 0),
(835, 5, 'GlennCcizBak', 0),
(839, 11, 'Roxas20', 2),
(841, 22, 'Slithered', 0),
(844, 4, 'Yukarie', 0),
(846, 11, 'RubberDuck', 1),
(847, 5, 'RubberDuck', 0),
(849, 25, 'JesseDoetsch', 1),
(850, 28, 'EvilYuber', 1),
(851, 29, 'alukardian', 0),
(852, 26, 'boko', 0),
(855, 22, 'alukardian', 0),
(856, 5, 'martbus', 0),
(858, 22, 'GlennCCizBak', 0),
(859, 15, 'martbus', 0),
(860, 11, 'JinVicious', 0),
(862, 12, 'HeranaShine', 1),
(863, 5, 'THeOneGod13', 0),
(864, 22, 'THeOneGod13', 1),
(866, 16, 'THeOneGod13', 1),
(867, 12, 'Derfas13', 1),
(868, 22, 'Zeromus', 0),
(869, 22, 'sockygti', 0),
(870, 4, 'RubberDuck', 0),
(872, 15, 'Slithered', 0),
(873, 26, 'VinnieHuynh', 1),
(876, 1, 'martbus', 0),
(877, 25, 'Dantushk', 0),
(880, 4, 'Norseman', 0),
(882, 22, 'Yukarie', 1),
(883, 1, 'MelloFello', 0),
(884, 15, 'Hashem', 0),
(885, 1, 'Hashem', 0),
(886, 1, 'ekibiogami', 2),
(887, 23, 'alukardian', 1),
(888, 1, 'alukardian', 0),
(889, 1, 'SeeDRankA420', 0),
(893, 2, 'cbowling', 0),
(894, 1, 'jackie', 0),
(895, 26, 'Yukarie', 0),
(896, 16, 'Slithered', 1),
(897, 1, 'Derfas13', 0),
(899, 5, 'MateriaThief', 0),
(900, 26, 'J102y', 0),
(903, 4, 'Rites000', 0),
(905, 23, 'martbus', 0),
(906, 22, 'Sunstar259', 5),
(907, 5, 'noobgore', 0),
(911, 26, 'squll-typo', 0),
(914, 22, 'crimiboss', 1),
(915, 23, 'crimiboss', 1),
(918, 15, 'IRPsyco', 0),
(919, 25, 'Yukarie', 0),
(920, 11, 'squll-typo', 1),
(921, 5, 'NanakiSeto', 0),
(922, 26, 'martbus', 1),
(924, 1, 'Jinvicious', 0),
(925, 12, 'Jinvicious', 0),
(926, 5, 'SolarEclipse', 0),
(927, 22, 'Roxas20', 1),
(933, 29, 'Juan2009', 0),
(934, 16, 'mizuryuu83', 0),
(935, 11, 'blade22', 0),
(936, 15, 'squll-typo', 0),
(938, 5, 'blade22', 0),
(940, 26, 'blade22', 0),
(943, 16, 'Nall', 0),
(944, 11, 'Nall', 0),
(946, 16, 'Slayerfortyk', 2),
(948, 23, 'SeeDRankA420', 1),
(949, 16, 'Yukarie', 0),
(951, 12, 'jonty', 0),
(953, 15, 'Roxas20', 0),
(954, 4, 'donnie3206', 0),
(956, 5, 'donnie3206', 0),
(957, 22, 'LordRaidor', 1),
(959, 11, 'leshrac99', 0),
(960, 23, 'LordRaidor', 1),
(961, 15, 'leshrac99', 0),
(962, 22, 'Crosswalker', 1),
(964, 1, 'george27', 0),
(965, 5, 'george27', 2),
(969, 4, 'squll-typo', 1),
(970, 4, 'hotshotryan', 0),
(971, 11, 'hotshotryan', 1),
(974, 16, 'george27', 0),
(976, 5, 'Turks', 1),
(977, 5, 'hotshotryan', 0),
(979, 4, 'kisoxxxiinch', 0),
(980, 15, 'Avarax', 0),
(981, 11, 'LordRaidor', 1),
(982, 29, 'martbus', 0),
(983, 23, 'squll-typo', 1),
(985, 27, 'squll-typo', 0),
(986, 13, 'squll-typo', 0),
(987, 26, 'Jinvicious', 0),
(989, 2, 'martbus', 0),
(993, 16, 'SeeDRankA420', 0),
(994, 11, 'noobgore', 0),
(995, 4, 'Nall', 0),
(998, 22, 'blade22', 0),
(1002, 23, 'DarkJaxyChan', 0),
(1005, 5, 'ladydeath', 1),
(1008, 22, 'martbus', 0),
(1009, 16, 'DarkJaxyChan', 0),
(1010, 11, 'kisoxxxiinch', 1),
(1011, 5, 'kisoxxxiinch', 15),
(1012, 22, 'Juan2009', 1),
(1013, 23, 'KisoxxXiinch', 1),
(1014, 25, 'squll-typo', 1),
(1015, 26, 'Roxas20', 1),
(1016, 16, 'caiman', 1),
(1017, 23, 'leshrac99', 0),
(1019, 12, 'Dioza', 1),
(1021, 4, 'elvoret659', 0),
(1022, 12, 'elvoret659', 0),
(1027, 26, 'Slayerfortyk', 1),
(1028, 22, 'Avarax', 2),
(1029, 23, 'Gilgamesh', 1),
(1030, 25, 'Ezekiel', 0),
(1032, 1, 'Zexion', 0),
(1034, 11, 'caiman', 1),
(1040, 4, 'brocknoth', 0),
(1042, 5, 'brocknoth', 0),
(1043, 12, 'Slayerfortyk', 1),
(1045, 16, 'GlennCCizBak', 1),
(1046, 16, 'kisoxxxiinch', 1),
(1047, 11, 'Espershiva', 1),
(1052, 25, 'Gringo335', 0),
(1053, 11, 'Jimmy3000', 1),
(1054, 4, 'Tyrannikos', 0),
(1056, 4, 'IKeiI', 0),
(1058, 2, 'Tyrannikos', 0),
(1059, 2, 'Kaizen', 0),
(1060, 5, 'kurosaki', 0),
(1061, 5, 'Athelston', 0),
(1062, 5, 'Tyrannikos', 0),
(1064, 5, 'EtherDrift', 0),
(1065, 22, 'caiman', 1),
(1066, 5, 'Mariyasha', 1),
(1067, 5, 'TimeCrash25', 0),
(1069, 11, 'ZackFairrr', 0),
(1070, 11, 'Derfas13', 1),
(1075, 12, 'EtherDrift', 0),
(1076, 5, 'IKeiI', 0),
(1077, 5, 'TheAfro', 1),
(1078, 5, 'Asgardian', 0),
(1079, 16, 'leshrac99', 0),
(1080, 23, 'EtherDrift', 0),
(1081, 16, 'madzell', 0),
(1082, 5, 'yitan', 0),
(1085, 1, 'carbonweapon', 0),
(1086, 4, 'Arkelsa', 0),
(1088, 5, 'Arkelsa', 0),
(1089, 23, 'Slayerfortyk', 1),
(1091, 16, 'YiKwang', 0),
(1092, 25, 'kisoxxxiinch', 1),
(1095, 22, 'YiKwang', 1),
(1096, 23, 'YiKwang', 1),
(1102, 4, 'anonymous', 0),
(1104, 4, 'VexedBlood', 0),
(1105, 25, 'VexedBlood', 1),
(1106, 15, 'YiKwang', 0),
(1107, 5, 'VexedBlood', 0),
(1109, 23, 'Vercery', 1),
(1110, 25, 'barrybright', 1),
(1111, 23, 'Espershiva', 1),
(1112, 4, 'Roxas20', 0),
(1113, 22, 'Doetsch', 0),
(1114, 5, 'Doetsch', 0),
(1115, 4, 'Doetsch', 1),
(1116, 2, 'Doetsch', 0),
(1117, 11, 'icey121', 0),
(1118, 16, 'Roxas20', 3),
(1119, 22, 'icey121', 0),
(1123, 4, 'YiKwang', 3),
(1125, 26, 'Necromance', 0),
(1126, 5, 'YiKwang', 4),
(1132, 11, 'YiKwang', 0),
(1133, 4, 'icey121', 0),
(1134, 4, 'pestycakes', 0),
(1135, 22, 'pestycakes', 1),
(1136, 22, 'TheGrimReape', 1),
(1137, 23, 'elvoret659', 1),
(1138, 25, 'Sunstar259', 4),
(1140, 2, 'jackie', 0),
(1141, 25, 'Roxas20', 1),
(1143, 17, 'wildspice', 1),
(1144, 26, 'YiKwang', 5),
(1145, 2, 'Ekibiogami', 1),
(1146, 12, 'YiKwang', 0),
(1147, 2, 'TaxB', 0),
(1148, 25, 'YiKwang', 1),
(1149, 2, 'pharoahsark', 1),
(1150, 3, 'jackie', 0),
(1151, 12, 'barrybright', 1),
(1152, 21, 'barrybright', 1),
(1153, 22, 'barrybright', 1),
(1154, 2, 'icey121', 0),
(1155, 16, 'shooterjack', 0),
(1156, 16, 'carbonweapon', 0),
(1157, 2, 'carbonweapon', 0),
(1158, 23, 'Agoni', 2),
(1159, 22, 'carbonweapon', 0),
(1160, 23, 'carbonweapon', 0),
(1161, 12, 'carbonweapon', 0),
(1162, 11, 'Slayerfortyk', 1),
(1163, 12, 'Donnie3206', 1),
(1164, 6, 'Fengsten', 1),
(1165, 1, 'KruxyMagik', 0),
(1166, 23, 'battousai98', 1),
(1167, 12, 'Roxas20', 2),
(1168, 17, 'J102y', 1),
(1169, 21, 'Ilir', 1),
(1170, 11, 'shooterjack', 0),
(1171, 23, 'XxJesterxX', 1),
(1172, 25, 'JinVicious', 1),
(1173, 11, 'ponyboyxiii', 0),
(1174, 18, 'sunstar259', 1),
(1175, 16, 'ponyboyxiii', 0),
(1176, 4, 'DragonStorm', 2),
(1177, 11, 'DragonStorm', 1),
(1178, 26, 'ponyboyxiii', 0),
(1179, 6, 'phoneshoe', 2),
(1180, 23, 'forumadmin', 1),
(1181, 23, 'TestUser1', 1),
(1182, 7, 'devileater', 0),
(1183, 5, 'devileater', 0),
(1184, 21, 'devileater', 1),
(1185, 18, 'devileater', 1),
(1186, 25, 'forumadmin', 1),
(1187, 16, 'forumadmin', 1),
(1188, 5, 'Cioud', 1),
(1189, 5, 'Tdex', 1);
