import React from 'react';
import {useMutation} from '@apollo/client';
import {Form, Input, message} from 'antd';
import {useNavigate} from 'react-router-dom';
import {ButtonCreate} from '../../../../../../components/ButtonCreate/ButtonCreate';
import 'moment/locale/uk';
import {sizeButtonItem, sizeFormItem} from '../../../../../../styles/form';
import {CREATE_SUBJECT_MUTATION, CreateSubjectData, CreateSubjectVars} from '../../subjects.mutations';

type FormValues = {
    name: string,
}

export const SubjectsCreate = () => {
    const [createSubjectMutation, createSubjectMutationOption] = useMutation<CreateSubjectData, CreateSubjectVars>(CREATE_SUBJECT_MUTATION);
    const [form] = Form.useForm();

    const navigate = useNavigate();

    const onFinish = async (values: FormValues) => {
        createSubjectMutation({variables: {createSubjectInputType: {...values}}})
            .then(response => {
                navigate('../');
            })
            .catch(error => {
                message.error(error.message);
            });
    };

    return (
        <Form
            name="SubjectsCreateForm"
            onFinish={onFinish}
            form={form}
            {...sizeFormItem}
        >
            <Form.Item
                name="name"
                label="Назва"
                rules={[{required: true, message: 'Введіть назву!'}]}
            >
                <Input placeholder="Назва"/>
            </Form.Item>
            <Form.Item {...sizeButtonItem}>
                <ButtonCreate loading={createSubjectMutationOption.loading} isSubmit={true}/>
            </Form.Item>
        </Form>
    );
};
