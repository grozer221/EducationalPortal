import React, {useState} from 'react';
import {useMutation, useQuery} from '@apollo/client';
import {Navigate, useNavigate, useParams} from 'react-router-dom';
import {Loading} from '../../../../../../components/Loading/Loading';
import {DatePicker, Form, Input, message} from 'antd';
import {ButtonUpdate} from '../../../../../../components/ButtonUpdate/ButtonUpdate';
import {sizeButtonItem, sizeFormItem} from '../../../../../../styles/form';
import {UPDATE_USER_MUTATION, UpdateUserData, UpdateUserVars} from '../../users.mutations';
import {
    GET_USER_QUERY,
    GET_USER_WITH_GRADE_QUERY,
    GetUserData,
    GetUserVars,
    GetUserWithGradeData,
    GetUserWithGradeVars,
} from '../../users.queries';
import moment from 'moment';
import {Role} from '../../users.types';
import {GET_GRADES_QUERY, GetGradesData, GetGradesVars} from '../../../grades/grades.queries';
import Title from 'antd/es/typography/Title';

type FormValues = {
    id: string,
    firstName: string,
    lastName: string,
    middleName: string,
    login: string,
    email: string,
    phoneNumber: string,
    dateOfBirth: any,
}

export const TeachersUpdate = () => {
    const params = useParams();
    const id = params.id as string;
    const [updateStudent, updateStudentOptions] = useMutation<UpdateUserData, UpdateUserVars>(UPDATE_USER_MUTATION);
    const [form] = Form.useForm();
    const getTeacher = useQuery<GetUserData, GetUserVars>(GET_USER_QUERY,
        {variables: {id: id}},
    );
    const navigate = useNavigate();

    const onFinish = async (values: FormValues) => {
        updateStudent({
            variables: {
                updateUserInputType: {
                    id: values.id,
                    firstName: values.firstName,
                    lastName: values.lastName,
                    middleName: values.middleName,
                    login: values.login,
                    email: values.email,
                    phoneNumber: values.phoneNumber,
                    dateOfBirth: new Date(values.dateOfBirth._d.setHours(12)).toISOString(),
                    role: Role.Teacher,
                    gradeId: undefined,
                },
            },
        })
            .then(response => {
                navigate('../');
            })
            .catch(error => {
                message.error(error.message);
            });
    };

    if (!id)
        return <Navigate to={'/error'}/>;

    if (getTeacher.loading)
        return <Loading/>;

    const student = getTeacher.data?.getUser;
    return (
        <Form
            name="TeachersUpdateForm"
            onFinish={onFinish}
            form={form}
            initialValues={{
                id: student?.id,
                firstName: student?.firstName,
                middleName: student?.middleName,
                lastName: student?.lastName,
                login: student?.login,
                email: student?.email,
                phoneNumber: student?.phoneNumber,
                dateOfBirth: moment(student?.dateOfBirth.split('T')[0], 'YYYY-MM-DD'),
            }}
            {...sizeFormItem}
        >
            <Title level={2}>Оновити учня</Title>
            <Form.Item name="id" style={{display: 'none'}}>
                <Input type={'hidden'}/>
            </Form.Item>
            <Form.Item
                name="firstName"
                label="Ім'я"
                rules={[{required: true, message: 'Введіть Ім\'я!'}]}
            >
                <Input placeholder="Ім'я"/>
            </Form.Item>
            <Form.Item
                name="middleName"
                label="По-батькові"
                rules={[{required: true, message: 'Введіть По-батькові!'}]}
            >
                <Input placeholder="По-батькові"/>
            </Form.Item>
            <Form.Item
                name="lastName"
                label="Прізвище"
                rules={[{required: true, message: 'Введіть Прізвище!'}]}
            >
                <Input placeholder="Прізвище"/>
            </Form.Item>
            <Form.Item
                name="login"
                label="Логін"
                rules={[{required: true, message: 'Введіть Логін!'}]}
            >
                <Input placeholder="Логін"/>
            </Form.Item>
            <Form.Item
                name="email"
                label="Email"
                rules={[{type: 'email', message: 'Невірно введено Email!'}]}
            >
                <Input placeholder="Email" type={'email'}/>
            </Form.Item>
            <Form.Item
                name="phoneNumber"
                label="Номер телефону"
            >
                <Input placeholder="Номер телефону"/>
            </Form.Item>
            <Form.Item
                name="dateOfBirth"
                label="Дата народження"
            >
                <DatePicker/>
            </Form.Item>
            <Form.Item {...sizeButtonItem}>
                <ButtonUpdate loading={updateStudentOptions.loading} isSubmit={true}/>
            </Form.Item>
        </Form>
    );
};
