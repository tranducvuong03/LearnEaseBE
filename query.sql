USE [LearnEase]

/*========================================================
  1. LANGUAGE & DIALECT
========================================================*/
INSERT INTO Languages(LanguageId, Name)
VALUES ('1A1D84D4-7E68-443D-8DFC-1E048C0ADF01', N'English');

INSERT INTO Dialects (DialectId, LanguageId, Name, Region, AccentCode, Description, VoiceSampleUrl, IsAvailable)
VALUES
  ('3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', '1A1D84D4-7E68-443D-8DFC-1E048C0ADF01',
   N'General American', N'United States', 'en-US',
   N'Standard American English accent',
   'https://cdn.learnEase.com/audio/accent/en_us_sample.mp3', 1),
  ('3B5F8475-29D7-4F2D-A127-8CBFDF8D83F2', '1A1D84D4-7E68-443D-8DFC-1E048C0ADF01',
   N'Received Pronunciation', N'United Kingdom', 'en-GB',
   N'Standard British English accent',
   'https://cdn.learnEase.com/audio/accent/en_gb_sample.mp3', 1),
  (NEWID(), '1A1D84D4-7E68-443D-8DFC-1E048C0ADF01', 'English (Australia)', 'Australia', 'en-AU', 'Australian English accent', NULL, 1),
  (NEWID(), '1A1D84D4-7E68-443D-8DFC-1E048C0ADF01', 'English (India)', 'India', 'en-IN', 'Indian English accent', NULL, 1),
  (NEWID(), '1A1D84D4-7E68-443D-8DFC-1E048C0ADF01', 'English (Canada)', 'Canada', 'en-CA', 'Canadian English accent', NULL, 1),
  (NEWID(), '1A1D84D4-7E68-443D-8DFC-1E048C0ADF01', 'English (New Zealand)', 'New Zealand', 'en-NZ', 'New Zealand English accent', NULL, 1),
  (NEWID(), '1A1D84D4-7E68-443D-8DFC-1E048C0ADF01', 'English (Singapore)', 'Singapore', 'en-SG', 'Singapore English accent', NULL, 1),
  (NEWID(), '1A1D84D4-7E68-443D-8DFC-1E048C0ADF01', 'English (South Africa)', 'South Africa', 'en-ZA', 'South African English accent', NULL, 1),
  (NEWID(), '1A1D84D4-7E68-443D-8DFC-1E048C0ADF01', 'English (Philippines)', 'Philippines', 'en-PH', 'Philippine English accent', NULL, 1),
  (NEWID(), '1A1D84D4-7E68-443D-8DFC-1E048C0ADF01', 'English (Hong Kong)', 'Hong Kong', 'en-HK', 'Hong Kong English accent', NULL, 1),
  (NEWID(), '1A1D84D4-7E68-443D-8DFC-1E048C0ADF01', 'English (Ireland)', 'Ireland', 'en-IE', 'Irish English accent', NULL, 1),
  (NEWID(), '1A1D84D4-7E68-443D-8DFC-1E048C0ADF01', 'English (Kenya)', 'Kenya', 'en-KE', 'Kenyan English accent', NULL, 1),
  (NEWID(), '1A1D84D4-7E68-443D-8DFC-1E048C0ADF01', 'English (Tanzania)', 'Tanzania', 'en-TZ', 'Tanzanian English accent', NULL, 1);

/*========================================================
  2. USER & SETTING
========================================================*/
INSERT INTO [Users]
  (UserId, Username, Email, Password, AvatarUrl, CreatedAt)
VALUES
  ('4C6EC7A2-644C-44B8-B222-52EDF6197781', 'vuong', 'vuong@example.com', '123', NULL, SYSUTCDATETIME());

INSERT INTO UserSettings
  (UserId, PreferredDialectId, PlaybackSpeed, NotificationOn)
VALUES
  ('4C6EC7A2-644C-44B8-B222-52EDF6197781', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 1.0, 1);

/*========================================================
  3. VOCABULARY
========================================================*/
INSERT INTO VocabularyItems
  (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson)
VALUES
  (NEWID(), '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'coffee', 'https://cdn.learnEase.com/audio/vocab/coffee_us.mp3', NULL, N'["toffee","coffer","copy"]'),
  (NEWID(), '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'library', 'https://cdn.learnEase.com/audio/vocab/library_us.mp3', NULL, N'["librarian","liberty","battery"]'),
  (NEWID(), '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'bicycle', 'https://cdn.learnEase.com/audio/vocab/bicycle_us.mp3', NULL, N'["tricycle","icicle","bisect"]'),
  (NEWID(), '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'vegetable', 'https://cdn.learnEase.com/audio/vocab/vegetable_us.mp3', NULL, N'["vegetation","vestibule","vegetarian"]'),
  (NEWID(), '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'airport', 'https://cdn.learnEase.com/audio/vocab/airport_us.mp3', NULL, N'["seaport","airplane","airdrop"]'),
  (NEWID(), '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'mountain', 'https://cdn.learnEase.com/audio/vocab/mountain_us.mp3', NULL, N'["fountain","maintain","molecule"]'),
  (NEWID(), '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'hospital', 'https://cdn.learnEase.com/audio/vocab/hospital_us.mp3', NULL, N'["hostile","hostel","postal"]'),
  (NEWID(), '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'elephant', 'https://cdn.learnEase.com/audio/vocab/elephant_us.mp3', NULL, N'["relevant","elegant","eleven"]'),
  (NEWID(), '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'umbrella', 'https://cdn.learnEase.com/audio/vocab/umbrella_us.mp3', NULL, N'["armadillo","umbrage","gazella"]'),
  (NEWID(), '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'computer', 'https://cdn.learnEase.com/audio/vocab/computer_us.mp3', NULL, N'["commuter","conductor","composer"]');

/*========================================================
  4. SPEAKING EXERCISE (with fixed ID)
========================================================*/
INSERT INTO SpeakingExercises
  (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText)
VALUES
  ('5D7E5B9D-1144-4AD2-B719-179B1A93AC01',
   '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1',
   N'Please say: "How are you today?"',
   'https://cdn.learnEase.com/audio/exercise/how_are_you_today.mp3',
   'How are you today?');

/*========================================================
  5. SPEAKING ATTEMPT
========================================================*/
INSERT INTO SpeakingAttempts
  (AttemptId, UserId, ExerciseId, AttemptedAt, Score, UserAudioUrl, Transcription)
VALUES
  ('AA1B2C3D-4E5F-6789-ABCD-EEFF00112233',
   '4C6EC7A2-644C-44B8-B222-52EDF6197781',
   '5D7E5B9D-1144-4AD2-B719-179B1A93AC01',
   SYSUTCDATETIME(), 85.0,
   'https://cdn.learnEase.com/audio/user/attempt_001.mp3',
   N'How are you today?');

/*========================================================
  6. ACHIEVEMENT & LEADERBOARD
========================================================*/
INSERT INTO Achievements
  (AchievementId, UserId, Name, Description, AchievedAt)
VALUES
  ('BBCCDD11-22EE-33FF-44AA-556677889900',
   '4C6EC7A2-644C-44B8-B222-52EDF6197781',
   N'First Lesson Completed',
   N'Hoàn thành bài học đầu tiên',
   DATEADD(HOUR,-2,SYSUTCDATETIME()));

INSERT INTO Leaderboards
  (LeaderboardId, UserId, Period, Score, RecordedAt)
VALUES
  ('CCDDEEFF-1122-3344-5566-77889900AABB',
   '4C6EC7A2-644C-44B8-B222-52EDF6197781',
   'weekly', 200,
   SYSUTCDATETIME());

/*========================================================
  7. AiLessons & AiLessonParts
========================================================*/
INSERT [dbo].[AiLessons] ([LessonId], [Topic], [CreatedAt], [DayIndex]) VALUES (N'a1dcd9b7-6db7-4270-b195-0eb536175289', N'Comparing Present Simple and Present Continuous Tenses', CAST(N'2025-07-02T09:26:43.7475610' AS DateTime2), 2)
INSERT [dbo].[AiLessons] ([LessonId], [Topic], [CreatedAt], [DayIndex]) VALUES (N'88b25f7c-a3f2-4e51-9799-34b6844a77c2', N'Comparing Present Simple and Present Continuous Tenses', CAST(N'2025-07-02T09:27:28.2168449' AS DateTime2), 6)
INSERT [dbo].[AiLessons] ([LessonId], [Topic], [CreatedAt], [DayIndex]) VALUES (N'9e9f6b5e-5822-4caa-b8e1-4238269e6ab4', N'Comparing Present Simple and Present Continuous Tenses', CAST(N'2025-07-02T09:27:37.3671289' AS DateTime2), 7)
INSERT [dbo].[AiLessons] ([LessonId], [Topic], [CreatedAt], [DayIndex]) VALUES (N'b28c3d0b-d6b5-428a-9d22-45f8ec0408c2', N'Comparing Present Simple and Present Continuous Tenses', CAST(N'2025-07-02T09:27:21.4634465' AS DateTime2), 5)
INSERT [dbo].[AiLessons] ([LessonId], [Topic], [CreatedAt], [DayIndex]) VALUES (N'ca0eeaf6-c847-42e8-aaf9-585f01f3d2b0', N'Comparing Present Simple and Present Continuous Tenses', CAST(N'2025-07-02T09:27:01.2432926' AS DateTime2), 3)
INSERT [dbo].[AiLessons] ([LessonId], [Topic], [CreatedAt], [DayIndex]) VALUES (N'ae447797-ef7d-400e-b862-83f6c2eef80d', N'Comparing Present Simple and Present Continuous Tenses', CAST(N'2025-07-02T09:27:10.7891748' AS DateTime2), 4)
INSERT [dbo].[AiLessons] ([LessonId], [Topic], [CreatedAt], [DayIndex]) VALUES (N'99ad5d69-998d-4f0a-b3c7-c482dc1c762f', N'Comparing Present Simple and Present Continuous Tenses', CAST(N'2025-07-02T09:26:30.8610478' AS DateTime2), 1)
GO
INSERT [dbo].[AiLessonParts] ([PartId], [LessonId], [Skill], [Prompt], [ReferenceText], [AudioUrl], [ChoicesJson]) VALUES (N'1babd4ba-1c8d-4cab-a622-104be8371424', N'b28c3d0b-d6b5-428a-9d22-45f8ec0408c2', 0, N'Listen to the audio and type exactly what you hear.', N'she usually reads books, but not today', N'https://storage.googleapis.com/learnease/d442bdba-8ac1-44fb-afb1-ccb36c65464c_da6239c5-976e-4439-88f0-31e656ac4007.mp3', NULL)
INSERT [dbo].[AiLessonParts] ([PartId], [LessonId], [Skill], [Prompt], [ReferenceText], [AudioUrl], [ChoicesJson]) VALUES (N'de517212-5d7e-4612-bf6e-11bcc055da81', N'b28c3d0b-d6b5-428a-9d22-45f8ec0408c2', 1, N'what activities do you usually do now versus weekends?', NULL, NULL, NULL)
INSERT [dbo].[AiLessonParts] ([PartId], [LessonId], [Skill], [Prompt], [ReferenceText], [AudioUrl], [ChoicesJson]) VALUES (N'924df427-51e3-478d-bba7-2471b6c9a8f5', N'88b25f7c-a3f2-4e51-9799-34b6844a77c2', 2, N'Read the passage and answer the questions.', N'a2–b1 learners often compare present simple and present continuous tenses in english. present simple is used for regular actions or general truths, while present continuous is used for actions happening now. for example, ''i study english every day'' (present simple) versus ''i am studying english right now'' (present continuous).', NULL, N'[
    {
      "question": "What is Present Simple used for?",
      "choices": ["A. Regular actions or general truths", "B. Actions happening now", "C. Past actions", "D. Future actions"],
      "answer": "A"
    },
    {
      "question": "Which tense is used for actions happening now?",
      "choices": ["A. Present Simple", "B. Present Continuous", "C. Past Perfect", "D. Future Perfect"],
      "answer": "B"
    },
    {
      "question": "In the sentence ''I am studying English right now'', which tense is being used?",
      "choices": ["A. Present Simple", "B. Present Continuous", "C. Past Simple", "D. Future Simple"],
      "answer": "B"
    }
  ]')
INSERT [dbo].[AiLessonParts] ([PartId], [LessonId], [Skill], [Prompt], [ReferenceText], [AudioUrl], [ChoicesJson]) VALUES (N'75dd6b0f-d568-4836-a382-25cdabe9fb56', N'9e9f6b5e-5822-4caa-b8e1-4238269e6ab4', 2, N'Read the passage and answer the questions.', N'a common challenge for english learners is understanding the difference between the present simple and present continuous tenses. the present simple is used for habits and routines, while the present continuous is used for actions happening now. for example, ''she eats fruit every day'' (present simple) and ''she is eating an apple right now'' (present continuous) showcase this contrast.', NULL, N'[
{"question": "When is the Present Simple tense typically used?", "choices": ["A. For habits and routines", "B. For actions happening now", "C. For future plans", "D. For past events"], "answer": "A"},
{"question": "Which tense is suitable for describing actions happening now?", "choices": ["A. Present Simple", "B. Past Simple", "C. Present Continuous", "D. Past Continuous"], "answer": "C"},
{"question": "In the sentence ''He ___ a book every night'', which tense should be used to fill in the blank?", "choices": ["A. reads", "B. is reading", "C. read", "D. reading"], "answer": "A"}
]')
INSERT [dbo].[AiLessonParts] ([PartId], [LessonId], [Skill], [Prompt], [ReferenceText], [AudioUrl], [ChoicesJson]) VALUES (N'31719140-1537-4bd6-81bb-29894a174278', N'ae447797-ef7d-400e-b862-83f6c2eef80d', 2, N'Read the passage and answer the questions.', N'a2-b1 learners often struggle with differentiating between the present simple and present continuous tenses in english. the present simple is used for habits and routines, while the present continuous is used for actions happening now. remember, the present simple is like a timetable and the present continuous is like a live broadcast.', NULL, N'[
    {
      "question": "When do we use the Present Simple tense?",
      "choices": ["A. For actions happening now", "B. For habits and routines", "C. For future plans", "D. For completed actions in the past"],
      "answer": "B"
    },
    {
      "question": "What is a characteristic of the Present Continuous tense?",
      "choices": ["A. Talking about general truths", "B. Describing past events", "C. Expressing future intentions", "D. Describing actions happening now"],
      "answer": "D"
    },
    {
      "question": "Which comparison best describes the difference between Present Simple and Present Continuous tenses?",
      "choices": ["A. Present Simple is continuous, Present Continuous is simple", "B. Present Simple is like a timetable, Present Continuous is like a live broadcast", "C. Present Simple is used for future actions, Present Continuous is used for past actions", "D. Present Simple is used for singular subjects, Present Continuous is used for plural subjects"],
      "answer": "B"
    }
  ]')
INSERT [dbo].[AiLessonParts] ([PartId], [LessonId], [Skill], [Prompt], [ReferenceText], [AudioUrl], [ChoicesJson]) VALUES (N'6cfc15cb-040f-48b0-aaec-2c3999734067', N'a1dcd9b7-6db7-4270-b195-0eb536175289', 3, N'describe a typical weekday using both present simple and present continuous tenses', NULL, NULL, NULL)
INSERT [dbo].[AiLessonParts] ([PartId], [LessonId], [Skill], [Prompt], [ReferenceText], [AudioUrl], [ChoicesJson]) VALUES (N'3fcd59f9-ff3f-43e4-bad9-3972a29ae5e7', N'99ad5d69-998d-4f0a-b3c7-c482dc1c762f', 1, N'what are you doing now? compare with your usual routine', NULL, NULL, NULL)
INSERT [dbo].[AiLessonParts] ([PartId], [LessonId], [Skill], [Prompt], [ReferenceText], [AudioUrl], [ChoicesJson]) VALUES (N'0eb936e5-bd8d-4019-b25f-3f6338b6b967', N'b28c3d0b-d6b5-428a-9d22-45f8ec0408c2', 3, N'describe your morning routine using both present simple and present continuous tenses', NULL, NULL, NULL)
INSERT [dbo].[AiLessonParts] ([PartId], [LessonId], [Skill], [Prompt], [ReferenceText], [AudioUrl], [ChoicesJson]) VALUES (N'02bca432-d783-4028-beb0-49a5f2184c7b', N'9e9f6b5e-5822-4caa-b8e1-4238269e6ab4', 3, N'describe your daily routine in present simple tense, then switch to present continuous to highlight temporary actions', NULL, NULL, NULL)
INSERT [dbo].[AiLessonParts] ([PartId], [LessonId], [Skill], [Prompt], [ReferenceText], [AudioUrl], [ChoicesJson]) VALUES (N'1e9c072b-b6f5-44c2-b163-4cd28282360d', N'88b25f7c-a3f2-4e51-9799-34b6844a77c2', 0, N'Listen to the audio and type exactly what you hear.', N'she often reads in the library, but today she''s dancing', N'https://storage.googleapis.com/learnease/aa92ff78-da5e-4f1b-8406-238e2989f819_d8be2697-e735-47f3-ada9-dc1fa078a124.mp3', NULL)
INSERT [dbo].[AiLessonParts] ([PartId], [LessonId], [Skill], [Prompt], [ReferenceText], [AudioUrl], [ChoicesJson]) VALUES (N'967884a2-689b-4a0c-8d71-4fb2ef173e47', N'99ad5d69-998d-4f0a-b3c7-c482dc1c762f', 3, N'describe a typical day using the present simple, then rewrite it in the present continuous tense', NULL, NULL, NULL)
INSERT [dbo].[AiLessonParts] ([PartId], [LessonId], [Skill], [Prompt], [ReferenceText], [AudioUrl], [ChoicesJson]) VALUES (N'29e04726-1e65-48bb-a8db-50b98de5b440', N'a1dcd9b7-6db7-4270-b195-0eb536175289', 1, N'what is john doing now compared to what he usually does?', NULL, NULL, NULL)
INSERT [dbo].[AiLessonParts] ([PartId], [LessonId], [Skill], [Prompt], [ReferenceText], [AudioUrl], [ChoicesJson]) VALUES (N'2a348213-6e1e-4202-90b1-7451bab6f328', N'99ad5d69-998d-4f0a-b3c7-c482dc1c762f', 0, N'Listen to the audio and type exactly what you hear.', N'he usually watches tv, but now he is studying', N'https://storage.googleapis.com/learnease/116af24b-4b23-4a66-99a4-cb6a248d2d8b_d8213d46-c68a-477a-96be-a1ef9ccdd481.mp3', NULL)
INSERT [dbo].[AiLessonParts] ([PartId], [LessonId], [Skill], [Prompt], [ReferenceText], [AudioUrl], [ChoicesJson]) VALUES (N'3731ec00-b60b-4d24-935d-752e6441ebad', N'ca0eeaf6-c847-42e8-aaf9-585f01f3d2b0', 2, N'Read the passage and answer the questions.', N'the present simple is used for habits and routines while the present continuous is used for actions happening now. for example, ''i eat breakfast every day'' (present simple) and ''i am eating lunch right now'' (present continuous). understanding these differences helps improve communication skills in english.', NULL, N'[
    {
      "question": "Which tense is used for habits and routines?",
      "choices": ["A) Present Simple", "B) Present Continuous", "C) Past Simple", "D) Past Continuous"],
      "answer": "A"
    },
    {
      "question": "Which tense is used for actions happening now?",
      "choices": ["A) Present Simple", "B) Present Continuous", "C) Past Simple", "D) Past Continuous"],
      "answer": "B"
    },
    {
      "question": "In which sentence is the Present Continuous tense correctly used?",
      "choices": ["A) She runs in the park every morning.", "B) They are reading a book at the moment.", "C) I drink coffee in the evening.", "D) He played football yesterday."],
      "answer": "B"
    }
  ]')
INSERT [dbo].[AiLessonParts] ([PartId], [LessonId], [Skill], [Prompt], [ReferenceText], [AudioUrl], [ChoicesJson]) VALUES (N'f2ea031c-fed0-4cfb-ba6b-7cc909f62931', N'ca0eeaf6-c847-42e8-aaf9-585f01f3d2b0', 3, N'describe your daily routine using the present simple tense, then switch to the present continuous to highlight specific actions', NULL, NULL, NULL)
INSERT [dbo].[AiLessonParts] ([PartId], [LessonId], [Skill], [Prompt], [ReferenceText], [AudioUrl], [ChoicesJson]) VALUES (N'b0132430-5f83-40e0-8232-85e6094545d3', N'ae447797-ef7d-400e-b862-83f6c2eef80d', 1, N'what activities do you usually do vs. are doing today?', NULL, NULL, NULL)
INSERT [dbo].[AiLessonParts] ([PartId], [LessonId], [Skill], [Prompt], [ReferenceText], [AudioUrl], [ChoicesJson]) VALUES (N'edef1025-b70d-491d-913d-8e78d701ea3b', N'ae447797-ef7d-400e-b862-83f6c2eef80d', 0, N'Listen to the audio and type exactly what you hear.', N'she usually reads books, but today she is watching tv', N'https://storage.googleapis.com/learnease/864b1837-114e-4020-b72a-a95689239d4f_6f52597d-1485-4bb0-9144-9133e4bde71d.mp3', NULL)
INSERT [dbo].[AiLessonParts] ([PartId], [LessonId], [Skill], [Prompt], [ReferenceText], [AudioUrl], [ChoicesJson]) VALUES (N'7f422ada-e7c1-46cd-b82b-a3081a0bd3a5', N'ae447797-ef7d-400e-b862-83f6c2eef80d', 3, N'describe your daily routine using the present simple tense, then switch to the present continuous to highlight ongoing actions', NULL, NULL, NULL)
INSERT [dbo].[AiLessonParts] ([PartId], [LessonId], [Skill], [Prompt], [ReferenceText], [AudioUrl], [ChoicesJson]) VALUES (N'6dcfc923-b9d4-4e4b-9f25-a661efbf2035', N'99ad5d69-998d-4f0a-b3c7-c482dc1c762f', 2, N'Read the passage and answer the questions.', N'a2-b1 learners often struggle with the differences between present simple and present continuous tenses. present simple is used for routine actions or facts, while present continuous is for actions happening now or temporary situations. practice is key to mastering these tenses.', NULL, N'[
    {
      "question": "Which tense is used for routine actions or facts?",
      "choices": ["A) Present Simple", "B) Present Continuous", "C) Past Simple", "D) Past Continuous"],
      "answer": "A"
    },
    {
      "question": "When do we use Present Continuous tense?",
      "choices": ["A) For actions happening now", "B) For routine actions", "C) For past events", "D) For permanent situations"],
      "answer": "A"
    },
    {
      "question": "What is essential to master Present Simple and Present Continuous tenses?",
      "choices": ["A) Consistent practice", "B) Random guessing", "C) Ignoring grammar rules", "D) Speaking in a different language"],
      "answer": "A"
    }
  ]')
INSERT [dbo].[AiLessonParts] ([PartId], [LessonId], [Skill], [Prompt], [ReferenceText], [AudioUrl], [ChoicesJson]) VALUES (N'faaaea65-f814-4d6a-8916-aafd2dfa9033', N'88b25f7c-a3f2-4e51-9799-34b6844a77c2', 3, N'describe what you are doing now and your daily routine in both the present simple and present continuous tenses', NULL, NULL, NULL)
INSERT [dbo].[AiLessonParts] ([PartId], [LessonId], [Skill], [Prompt], [ReferenceText], [AudioUrl], [ChoicesJson]) VALUES (N'02da7d37-af39-42be-90d1-b36e119aa757', N'a1dcd9b7-6db7-4270-b195-0eb536175289', 2, N'Read the passage and answer the questions.', N'a2–b1 learners often confuse the present simple and present continuous tenses in english. the present simple is used for routines and habits, while the present continuous is used for actions happening now. remember, ''i play tennis'' (present simple) vs. ''i am playing tennis'' (present continuous). practice using both tenses to improve your english skills.', NULL, N'[
    {
      "question": "Which tense is used for routines and habits?",
      "choices": ["A) Present Simple", "B) Present Continuous", "C) Past Simple", "D) Future Simple"],
      "answer": "A"
    },
    {
      "question": "Which tense is used for actions happening now?",
      "choices": ["A) Present Simple", "B) Present Continuous", "C) Past Simple", "D) Future Simple"],
      "answer": "B"
    },
    {
      "question": "Choose the correct sentence using the Present Continuous tense:",
      "choices": ["A) He runs every morning.", "B) They are playing football at the moment.", "C) She doesn''t like bananas.", "D) We will go to the beach tomorrow."],
      "answer": "B"
    }
  ]')
INSERT [dbo].[AiLessonParts] ([PartId], [LessonId], [Skill], [Prompt], [ReferenceText], [AudioUrl], [ChoicesJson]) VALUES (N'637a7d31-df4d-4924-848e-bc9a8944ca36', N'9e9f6b5e-5822-4caa-b8e1-4238269e6ab4', 0, N'Listen to the audio and type exactly what you hear.', N'she always reads in the library but is studying now', N'https://storage.googleapis.com/learnease/e5ae0758-2b1e-4e05-9210-97b913de09a3_2977240e-387f-4a04-b33f-b0cbec835c89.mp3', NULL)
INSERT [dbo].[AiLessonParts] ([PartId], [LessonId], [Skill], [Prompt], [ReferenceText], [AudioUrl], [ChoicesJson]) VALUES (N'a7450034-c2fe-4833-8376-caa680226eb0', N'ca0eeaf6-c847-42e8-aaf9-585f01f3d2b0', 0, N'Listen to the audio and type exactly what you hear.', N'she often reads books, but today she is watching a movie', N'https://storage.googleapis.com/learnease/99fa4918-2e28-472f-abad-13fc01595f74_a8ca1ca5-3ef2-4349-9738-9ee2ef2db9b7.mp3', NULL)
INSERT [dbo].[AiLessonParts] ([PartId], [LessonId], [Skill], [Prompt], [ReferenceText], [AudioUrl], [ChoicesJson]) VALUES (N'99cff458-cfb1-4ae1-810d-e0fed8f0dc2f', N'a1dcd9b7-6db7-4270-b195-0eb536175289', 0, N'Listen to the audio and type exactly what you hear.', N'she always paints on sundays, but she is drawing tonight', N'https://storage.googleapis.com/learnease/c96aeab9-5ec7-40da-ae29-0f4276838149_cf0b3697-0f5c-4e0b-a50a-0349769951bd.mp3', NULL)
INSERT [dbo].[AiLessonParts] ([PartId], [LessonId], [Skill], [Prompt], [ReferenceText], [AudioUrl], [ChoicesJson]) VALUES (N'2afaf492-f3af-43af-a393-e83ed295d83a', N'9e9f6b5e-5822-4caa-b8e1-4238269e6ab4', 1, N'what is the difference between "i play" and "i am playing"?', NULL, NULL, NULL)
INSERT [dbo].[AiLessonParts] ([PartId], [LessonId], [Skill], [Prompt], [ReferenceText], [AudioUrl], [ChoicesJson]) VALUES (N'89dfe735-ed1f-473e-8f69-ebffdb2cadae', N'b28c3d0b-d6b5-428a-9d22-45f8ec0408c2', 2, N'Read the passage and answer the questions.', N'a2-b1 learners often struggle with the differences between the present simple and present continuous tenses in english. the present simple is used for habitual actions or general truths, while the present continuous is used for actions happening now or future arrangements. it''s important to pay attention to keywords like ''always'' for present simple and ''now'' for present continuous.', NULL, N'[
    {
      "question": "Which tense is used for habitual actions or general truths?",
      "choices": ["A) Present Simple", "B) Present Continuous", "C) Past Simple", "D) Future Simple"],
      "answer": "A"
    },
    {
      "question": "Which tense is used for actions happening now or future arrangements?",
      "choices": ["A) Present Simple", "B) Present Continuous", "C) Past Continuous", "D) Future Simple"],
      "answer": "B"
    },
    {
      "question": "What kind of keywords should learners pay attention to when using the Present Continuous tense?",
      "choices": ["A) Past tense keywords", "B) Future tense keywords", "C) Continuous tense keywords", "D) ''now'' and ''future'' keywords"],
      "answer": "D"
    }
  ]')
INSERT [dbo].[AiLessonParts] ([PartId], [LessonId], [Skill], [Prompt], [ReferenceText], [AudioUrl], [ChoicesJson]) VALUES (N'44a2f0a5-6871-4067-a48f-f867cd5b0172', N'88b25f7c-a3f2-4e51-9799-34b6844a77c2', 1, N'when do you use present simple and present continuous tenses?', NULL, NULL, NULL)
INSERT [dbo].[AiLessonParts] ([PartId], [LessonId], [Skill], [Prompt], [ReferenceText], [AudioUrl], [ChoicesJson]) VALUES (N'8c84d4a8-53c4-47b1-91f0-fcc7ade6b6c8', N'ca0eeaf6-c847-42e8-aaf9-585f01f3d2b0', 1, N'do you prefer reading books or watching movies in english?', NULL, NULL, NULL)
GO

/*========================================================
  8. TOPIC
========================================================*/
INSERT INTO Topic (TopicId, Title, Description, [Order])
VALUES
  ('11111111-1111-1111-1111-111111111111', N'Greetings', N'Learn how to greet people in different situations.', 1),
  ('22222222-2222-2222-2222-222222222222', N'Daily Routine', N'Vocabulary and speaking related to everyday daily habits.', 2),
  ('33333333-3333-3333-3333-333333333333', N'Food & Drink', N'Words and phrases for food, meals, eating out and drinks.', 3),
  ('44444444-4444-4444-4444-444444444444', N'Work & Office', N'Essential vocabulary for professional and workplace communication.', 4),
  ('55555555-5555-5555-5555-555555555555', N'Travel', N'Language and expressions for traveling, directions and transportation.', 5);
  /*========================================================
  9. LESSON (5 lesson mỗi topic)
========================================================*/
INSERT INTO Lessons(LessonId, TopicId, DialectId, [Order], Title)
VALUES
-- Greetings Topic (ID: 1111...)
(NEWID(), '11111111-1111-1111-1111-111111111111', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 1, N'Greeting Basics'),
(NEWID(), '11111111-1111-1111-1111-111111111111', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 2, N'Formal Greetings'),
(NEWID(), '11111111-1111-1111-1111-111111111111', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 3, N'Casual Greetings'),
(NEWID(), '11111111-1111-1111-1111-111111111111', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 4, N'Introducing Yourself'),
(NEWID(), '11111111-1111-1111-1111-111111111111', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 5, N'Farewells'),

-- Daily Routine Topic (ID: 2222...)
(NEWID(), '22222222-2222-2222-2222-222222222222', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 1, N'Morning Activities'),
(NEWID(), '22222222-2222-2222-2222-222222222222', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 2, N'Daily Chores'),
(NEWID(), '22222222-2222-2222-2222-222222222222', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 3, N'Work and Study'),
(NEWID(), '22222222-2222-2222-2222-222222222222', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 4, N'Evening Routine'),
(NEWID(), '22222222-2222-2222-2222-222222222222', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 5, N'Weekend Activities'),

-- Food & Drink Topic (ID: 3333...)
(NEWID(), '33333333-3333-3333-3333-333333333333', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 1, N'Common Foods'),
(NEWID(), '33333333-3333-3333-3333-333333333333', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 2, N'Drinks and Beverages'),
(NEWID(), '33333333-3333-3333-3333-333333333333', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 3, N'At the Restaurant'),
(NEWID(), '33333333-3333-3333-3333-333333333333', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 4, N'Likes and Dislikes'),
(NEWID(), '33333333-3333-3333-3333-333333333333', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 5, N'Food Quantities'),

-- Work & Office Topic (ID: 4444...)
(NEWID(), '44444444-4444-4444-4444-444444444444', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 1, N'Office Vocabulary'),
(NEWID(), '44444444-4444-4444-4444-444444444444', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 2, N'Meeting Language'),
(NEWID(), '44444444-4444-4444-4444-444444444444', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 3, N'Writing Emails'),
(NEWID(), '44444444-4444-4444-4444-444444444444', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 4, N'Job Interviews'),
(NEWID(), '44444444-4444-4444-4444-444444444444', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 5, N'Describing Jobs'),

-- Travel Topic (ID: 5555...)
(NEWID(), '55555555-5555-5555-5555-555555555555', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 1, N'At the Airport'),
(NEWID(), '55555555-5555-5555-5555-555555555555', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 2, N'Hotel Conversations'),
(NEWID(), '55555555-5555-5555-5555-555555555555', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 3, N'Asking for Directions'),
(NEWID(), '55555555-5555-5555-5555-555555555555', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 4, N'Transportation'),
(NEWID(), '55555555-5555-5555-5555-555555555555', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 5, N'Travel Problems');

