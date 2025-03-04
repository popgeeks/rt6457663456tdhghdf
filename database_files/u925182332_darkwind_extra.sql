
--
-- Indexes for dumped tables
--

--
-- Indexes for table `veteran_advancement`
--
ALTER TABLE `veteran_advancement`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `wantedmarketplace`
--
ALTER TABLE `wantedmarketplace`
  ADD PRIMARY KEY (`WantedMarketPlaceID`),
  ADD KEY `idxPlayerID` (`PlayerID`),
  ADD KEY `idxCardID` (`CardID`),
  ADD KEY `idxFilledBy` (`FilledBy`);

--
-- Indexes for table `warns`
--
ALTER TABLE `warns`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `webguests`
--
ALTER TABLE `webguests`
  ADD PRIMARY KEY (`ID`);

--
-- Indexes for table `weblogin`
--
ALTER TABLE `weblogin`
  ADD PRIMARY KEY (`ID`);

--
-- Indexes for table `webusers`
--
ALTER TABLE `webusers`
  ADD PRIMARY KEY (`ID`);

--
-- Indexes for table `wordfilter`
--
ALTER TABLE `wordfilter`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `veteran_advancement`
--
ALTER TABLE `veteran_advancement`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT for table `wantedmarketplace`
--
ALTER TABLE `wantedmarketplace`
  MODIFY `WantedMarketPlaceID` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2154;

--
-- AUTO_INCREMENT for table `warns`
--
ALTER TABLE `warns`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=1523;

--
-- AUTO_INCREMENT for table `webguests`
--
ALTER TABLE `webguests`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=39866;

--
-- AUTO_INCREMENT for table `weblogin`
--
ALTER TABLE `weblogin`
  MODIFY `ID` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=1795;

--
-- AUTO_INCREMENT for table `webusers`
--
ALTER TABLE `webusers`
  MODIFY `ID` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `wordfilter`
--
ALTER TABLE `wordfilter`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=94;
