import React from 'react';
import {useMutation} from '@apollo/client';
import {DatePicker, Form, Input, message} from 'antd';
import {useNavigate} from 'react-router-dom';
import {ButtonCreate} from '../../../../../../components/ButtonCreate/ButtonCreate';
import {sizeButtonItem, sizeFormItem} from '../../../../../../styles/form';
import {CREATE_USER_MUTATION, CreateUserData, CreateUserVars} from '../../../../../../graphQL/modules/users/users.mutations';
import CyrillicToTranslit from 'cyrillic-to-translit-js';
import {Role} from '../../../../../../graphQL/modules/users/users.types';
import Title from 'antd/es/typography/Title';
import {ukDateFormat} from '../../../../../../utils/formats';
import 'moment/locale/uk';

// @ts-ignore
const cyrillicToTranslit = new CyrillicToTranslit({preset: 'uk'});

type FormValues = {
    firstName: string,
    lastName: string,
    middleName: string,
    login: string,
    password: string,
    email: string,
    phoneNumber: string,
    dateOfBirth: any,
}

export const TeachersCreate = () => {
    const [createStudentMutation, createStudentMutationOption] = useMutation<CreateUserData, CreateUserVars>(CREATE_USER_MUTATION);
    const [form] = Form.useForm();
    const navigate = useNavigate();

    const onFinish = async (values: FormValues) => {
        createStudentMutation({
            variables: {
                createUserInputType: {
                    firstName: values.firstName,
                    lastName: values.lastName,
                    middleName: values.middleName,
                    login: values.login,
                    password: values.password,
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

    const changeLogin = () => {
        const lastName: string = form.getFieldValue('lastName') || '';
        const lastName1Letter = lastName.length ? lastName[0] : '';
        const firstName: string = form.getFieldValue('firstName') || '';
        const firstName1Letter = firstName.length ? firstName[0] : '';
        const middleName: string = form.getFieldValue('middleName') || '';
        const middleName1Letter = middleName.length ? middleName[0] : '';
        const dateOfBorn: Date | null = form.getFieldValue('dateOfBirth')?._d;
        const yearOfBorn = dateOfBorn?.getFullYear() || '';
        form.setFieldsValue({
            login: cyrillicToTranslit.transform(`${yearOfBorn}_${lastName1Letter}${firstName1Letter}${middleName1Letter}`).toLowerCase(),
        });
    };

    return (
        <Form
            name="TeachersCreateForm"
            onFinish={onFinish}
            form={form}
            initialValues={{
                password: (Math.random() + 1).toString(36).substring(6),
            }}
            {...sizeFormItem}
        >
            <Title level={2}>???????????????? ??????????????</Title>
            <Form.Item
                name="lastName"
                label="????????????????"
                rules={[{required: true, message: '?????????????? ????????????????!'}]}
            >
                <Input placeholder="????????????????" onChange={() => changeLogin()}/>
            </Form.Item>
            <Form.Item
                name="firstName"
                label="????'??"
                rules={[{required: true, message: '?????????????? ????\'??!'}]}
            >
                <Input placeholder="????'??" onChange={() => changeLogin()}/>
            </Form.Item>
            <Form.Item
                name="middleName"
                label="????-????????????????"
                rules={[{required: true, message: '?????????????? ????-????????????????!'}]}
            >
                <Input placeholder="????-????????????????" onChange={() => changeLogin()}/>
            </Form.Item>
            <Form.Item
                name="login"
                label="??????????"
                rules={[{required: true, message: '?????????????? ??????????!'}]}
            >
                <Input placeholder="??????????"/>
            </Form.Item>
            <Form.Item
                name="password"
                label="????????????"
                rules={[{required: true, message: '?????????????? ????????????!'}]}
            >
                <Input placeholder="????????????"/>
            </Form.Item>
            <Form.Item
                name="email"
                label="Email"
                rules={[{type: 'email', message: '?????????????? ?????????????? Email!'}]}
            >
                <Input placeholder="Email" type={'email'}/>
            </Form.Item>
            <Form.Item
                name="phoneNumber"
                label="?????????? ????????????????"
            >
                <Input placeholder="?????????? ????????????????"/>
            </Form.Item>
            <Form.Item
                name="dateOfBirth"
                label="???????? ????????????????????"
            >
                <DatePicker format={ukDateFormat} onChange={() => changeLogin()}/>
            </Form.Item>
            <Form.Item {...sizeButtonItem}>
                <ButtonCreate loading={createStudentMutationOption.loading} isSubmit={true}/>
            </Form.Item>
        </Form>
    );
};
