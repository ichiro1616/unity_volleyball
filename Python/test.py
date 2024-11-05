# # import math

# # # 定数
# # m = 0.27  # バレーボールの質量 (kg)
# # r = 0.105  # バレーボールの半径 (m)
# # t = 0.1  # トルクが加えられる時間 (s)

# # # 慣性モーメントの計算
# # I = (2 / 5) * m * r**2

# # def calculate_torque(rpm):
# #     # 回転数から角速度の計算
# #     omega = (rpm * 2 * math.pi) / 60
# #     # インパルスの計算
# #     J = I * omega
# #     # トルクの計算
# #     T = J / t
# #     return T

# # # 各回転数に対するトルクを計算
# # rpm_values = [2000, 3000, 0, 200]
# # torque_values = [calculate_torque(rpm) for rpm in rpm_values]

# # print(torque_values)


#  if(speedInMetersPerSecond == 120.0){
#             Debug.Log("終了");
#             Application.Quit();
#         }
#         if(rotationAnglePhi == 30.0){
#             launchAngleTheta += 10.0f;
#             rotationAnglePhi = -30.0f;
#         }else{
#             rotationAnglePhi += 5.0f;
#         }
#         if(launchAngleTheta == 90.0){
#             speedInMetersPerSecond += 10.0f/3.6f;
#             launchAngleTheta = 0.0f;
        # }
# a = 40
# b = 0
# c =-12
# while True:

#     if a == 44:
#         break
#     if c == 10:
#         b += 2
#         c = -10
#     else:
#         c += 2
        
#     if b== 32:
#         a += 2
#         b = 0
#     print(a,b,c)
    

# import numpy as np

# # 元のデータ (float型に変換)
# data = np.array([30, 40, 60, 80, 50, 70, 90, 10, 20], dtype=float)
# print(data)

# # ラグを適用したシフトされたデータを格納する辞書
# shifted_data = {}

# # 正のラグ（1~10）
# for lag in range(1, 11):
#     # シフトされたデータの計算（後ろにずらす）
#     shifted = np.roll(data, lag)
#     shifted[:lag] = np.nan  # シフトによって生じる空白部分をNaNに
#     shifted_data[f'Lag {lag}'] = shifted

# # 負のラグ（-1~-10）
# for lag in range(1, 11):
#     # シフトされたデータの計算（前にずらす）
#     shifted = np.roll(data, -lag)
#     shifted[-lag:] = np.nan  # シフトによって生じる空白部分をNaNに
#     shifted_data[f'Lag -{lag}'] = shifted

# # シフトされたデータを出力
# for key, value in shifted_data.items():
#     print(f"{key}: {value}")

    